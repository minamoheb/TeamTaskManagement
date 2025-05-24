

namespace TeamTaskManagement.Services.Helper.Configuration
{
    public class SystemSettingsConfiguration : IDisposable
    {
        public AppSettingsConfiguration AppSettingsConfiguration { get; set; }
        public string[] CorsUrls { get; set; }

        public void Dispose()
        {
        }
    }
}
