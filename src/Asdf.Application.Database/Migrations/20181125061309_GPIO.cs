using Microsoft.EntityFrameworkCore.Migrations;

namespace Asdf.Application.Database.Migrations
{
    public partial class GPIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NodeTemplates",
                columns: new[] { "Id", "ActivatorAssembly", "ActivatorType", "Name" },
                values: new object[] { 6L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.GpioNode", "GPIO" });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[] { 19L, "Name", 6L, "System.String", null });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[] { 20L, "Gpio", 6L, "System.String", null });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[] { 21L, "Status", 6L, "Asdf.Domain.Actions.GpioStatusType", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldTemplates",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "FieldTemplates",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "FieldTemplates",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "NodeTemplates",
                keyColumn: "Id",
                keyValue: 6L);
        }
    }
}
