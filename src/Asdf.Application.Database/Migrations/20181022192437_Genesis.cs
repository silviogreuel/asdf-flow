using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Asdf.Application.Database.Migrations
{
    public partial class Genesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ActivatorType = table.Column<string>(nullable: true),
                    ActivatorAssembly = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Pin = table.Column<int>(nullable: true),
                    Token = table.Column<Guid>(nullable: true),
                    AuthId = table.Column<long>(nullable: true),
                    AuthProvider = table.Column<string>(nullable: true),
                    JwtToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NodeTemplateId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldTemplates_NodeTemplates_NodeTemplateId",
                        column: x => x.NodeTemplateId,
                        principalTable: "NodeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Token = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    PassId = table.Column<long>(nullable: true),
                    FailId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Left = table.Column<string>(nullable: true),
                    Right = table.Column<string>(nullable: true),
                    Operation = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    HttpPostNode_Url = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Device = table.Column<Guid>(nullable: true),
                    MqttPublishNode_Field = table.Column<string>(nullable: true),
                    TemplateNode_Field = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_Nodes_FailId",
                        column: x => x.FailId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nodes_Nodes_PassId",
                        column: x => x.PassId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    RootId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Context = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triggers_Nodes_RootId",
                        column: x => x.RootId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Triggers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    TriggerId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flows_Triggers_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flows_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NodeTemplates",
                columns: new[] { "Id", "ActivatorAssembly", "ActivatorType", "Name" },
                values: new object[,]
                {
                    { 1L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.HttpGetNode", "HTTP GET" },
                    { 2L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.HttpPostNode", "HTTP POST" },
                    { 3L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.AttributeNode", "ATTRIBUTE" },
                    { 4L, "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Asdf.Domain.Actions.MqttPublishNode", "MQTT PUBLISH" }
                });

            migrationBuilder.InsertData(
                table: "FieldTemplates",
                columns: new[] { "Id", "Name", "NodeTemplateId", "Type", "Value" },
                values: new object[,]
                {
                    { 1L, "Name", 1L, "System.String", null },
                    { 2L, "Url", 1L, "System.String", null },
                    { 3L, "Field", 1L, "System.String", null },
                    { 4L, "Name", 2L, "System.String", null },
                    { 5L, "Url", 2L, "System.String", null },
                    { 6L, "Content", 2L, "System.String", null },
                    { 7L, "Content-Type", 2L, "System.String", null },
                    { 8L, "Name", 3L, "System.String", null },
                    { 9L, "Key", 3L, "System.String", null },
                    { 10L, "Value", 3L, "System.String", null },
                    { 11L, "Name", 4L, "System.String", null },
                    { 12L, "Device", 4L, "System.Guid", null },
                    { 13L, "Field", 4L, "System.String", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTemplates_NodeTemplateId",
                table: "FieldTemplates",
                column: "NodeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_TriggerId",
                table: "Flows",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_UserId",
                table: "Flows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_FailId",
                table: "Nodes",
                column: "FailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_PassId",
                table: "Nodes",
                column: "PassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_UserId",
                table: "Nodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_RootId",
                table: "Triggers",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_UserId",
                table: "Triggers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "FieldTemplates");

            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropTable(
                name: "NodeTemplates");

            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
