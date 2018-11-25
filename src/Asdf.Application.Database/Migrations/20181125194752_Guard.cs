using Microsoft.EntityFrameworkCore.Migrations;

namespace Asdf.Application.Database.Migrations
{
    public partial class Guard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NodeTemplates",
                columns: new[] { "Id", "ActivatorAssembly", "ActivatorType", "Name" },
                values: new object[] { 7L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.GuardNode", "GUARD" });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[] { 22L, "Name", 7L, "System.String", null });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[] { 23L, "Field", 7L, "System.String", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldTemplates",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "FieldTemplates",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "NodeTemplates",
                keyColumn: "Id",
                keyValue: 7L);
        }
    }
}
