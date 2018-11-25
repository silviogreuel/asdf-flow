using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asdf.Application.Database.Migrations
{
    public partial class GpioGuard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gpio",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HttpGetNode_Field",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MqttPublishNode_Device",
                table: "Nodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gpio",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "HttpGetNode_Field",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "MqttPublishNode_Device",
                table: "Nodes");
        }
    }
}
