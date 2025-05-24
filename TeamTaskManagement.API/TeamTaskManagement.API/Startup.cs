using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TeamTaskManagement.API.Hubs;
using TeamTaskManagement.API.Middleware;
using TeamTaskManagement.Infrastructure;
using TeamTaskManagement.Infrastructure.UnitOfWork;
using TeamTaskManagement.Services.Helper.Configuration;
namespace TeamTaskManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region *****Get System Setting*****
            var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
            var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbsource = Environment.GetEnvironmentVariable("DB_SOURCE");

            var systemSettings = new SystemSettingsConfiguration();
            Configuration.Bind(key: nameof(systemSettings), systemSettings);
            services.AddSingleton(systemSettings);

            var connectionDb = systemSettings.AppSettingsConfiguration.ConnectionStrings;

            var connectionstring = connectionDb
                    .Replace("{DB_USERNAME}", dbUser)
                    .Replace("{DB_PASSWORD}", dbPass)
                    .Replace("{DB_NAME}", dbName)
                    .Replace("{DB_SOURCE}", dbsource);
            #endregion
            #region *****DbContext*****
            services.AddDbContext<TeamTaskServicesDBContext>(m => m.UseSqlServer(connectionstring, b => b.MigrationsAssembly(typeof(TeamTaskServicesDBContext).Assembly.FullName)), ServiceLifetime.Singleton);

            services.AddScoped<DbContext, TeamTaskServicesDBContext>();
            #endregion

            #region *****Unit Of Work*****
            services.AddScoped<IUnitOfWork<TeamTaskServicesDBContext>, UnitOfWork<TeamTaskServicesDBContext>>();
            #endregion
            services.AddApplicationServices();
            services.AddSignalR();



            var Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.MSSqlServer(
          connectionString: connectionstring,
          tableName: "Logs",
          autoCreateSqlTable: true)
        .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });




            services.AddCors(options =>
                 {
                     options.AddPolicy("AllowSpecificOrigins",
                     builder =>
                     {
                         var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
                         builder.WithOrigins(allowedOrigins)
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials()
                     .WithExposedHeaders("Content-Disposition");

                     });
                 });



            #region  *********** Sawager******
            if (systemSettings.AppSettingsConfiguration.enableSwagger)
            {
                services.AddSwaggerGen((swagger) =>
          {
              //This is to generate the Default UI of Swagger Documentation
              swagger.SwaggerDoc("v1", new OpenApiInfo
              {
                  Version = "v1",
                  Title = "External_Portal Api",
                  Description = "Authentication and Authorization Portal Api with JWT and Swagger",
              });
              // To Enable authorization using Swagger (JWT)
              swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
              {
                  Name = "Authorization",
                  Type = SecuritySchemeType.ApiKey,
                  Scheme = "Bearer",
                  BearerFormat = "JWT",
                  In = ParameterLocation.Header,
                  Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
              });
              swagger.AddSecurityRequirement(new OpenApiSecurityRequirement{
                  {
                      new OpenApiSecurityScheme
                      {
                          Reference = new OpenApiReference
                          {
                              Type = ReferenceType.SecurityScheme,
                              Id = "Bearer"
                          }
                      },
                          new string[] {}
                      }
              });
          });
            }

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureCustomExceptionMiddleware();
            if (env.IsDevelopment())
            {

            }
            else
            {
                //app.ConfigureCustomExceptionMiddleware();
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TeamTaskServicesDBContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigins");
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
      {
          endpoints.MapControllers();
          endpoints.MapHub<ChatHub>("/chathub");
      });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CTS.API v1"));
        }
    }
}
