using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TurntablRoleManager.API.Migrations
{
    public partial class seedEmployeeRoleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "EmployeeId", "Id", "RoleId" },
                values: new object[] { 1, new Guid("7c4854f2-bbfc-4d5a-88fa-9fe19e480bc0"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumns: new[] { "EmployeeId", "Id" },
                keyValues: new object[] { 1, new Guid("7c4854f2-bbfc-4d5a-88fa-9fe19e480bc0") });
        }
    }
}
