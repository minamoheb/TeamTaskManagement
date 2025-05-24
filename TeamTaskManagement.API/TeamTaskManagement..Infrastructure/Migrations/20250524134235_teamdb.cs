using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamTaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class teamdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookupItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookupItems_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    PriorityId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_LookupItems_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "LookupItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_LookupItems_StatusId",
                        column: x => x.StatusId,
                        principalTable: "LookupItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "ModifiedBy", "ModifiedDate", "Name", "NameAR" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9369), true, null, null, "Status", "الحالة" },
                    { 2L, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9422), true, null, null, "Priority", "الأولوية" }
                });

            migrationBuilder.InsertData(
                table: "LookupItems",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "IsActive", "LookupId", "ModifiedBy", "ModifiedDate", "NameAR", "NameEn" },
                values: new object[,]
                {
                    { 1L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9845), true, 1L, null, null, "مفتوح", "Open" },
                    { 2L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9855), true, 1L, null, null, "قيد التنفيذ", "In Progress" },
                    { 3L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9857), true, 1L, null, null, "مغلق", "Closed" },
                    { 4L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9859), true, 2L, null, null, "منخفض", "Low" },
                    { 5L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9861), true, 2L, null, null, "متوسط", "Medium" },
                    { 6L, null, 1L, new DateTime(2025, 5, 24, 16, 42, 35, 121, DateTimeKind.Local).AddTicks(9907), true, 2L, null, null, "مرتفع", "High" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_LookupId",
                table: "LookupItems",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "LookupItems");

            migrationBuilder.DropTable(
                name: "Lookups");
        }
    }
}
