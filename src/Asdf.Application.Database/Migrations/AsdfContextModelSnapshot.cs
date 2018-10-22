﻿// <auto-generated />
using System;
using Asdf.Application.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Asdf.Application.Database.Migrations
{
    [DbContext(typeof(AsdfContext))]
    partial class AsdfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Asdf.Domain.Actions.Node", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<long?>("FailId");

                    b.Property<string>("Name");

                    b.Property<long?>("PassId");

                    b.HasKey("Id");

                    b.HasIndex("FailId")
                        .IsUnique();

                    b.HasIndex("PassId")
                        .IsUnique();

                    b.ToTable("Nodes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Node");
                });

            modelBuilder.Entity("Asdf.Domain.Devices.Device", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("Token");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Asdf.Domain.Flows.Flow", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("TriggerId");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("Asdf.Domain.Templates.FieldTemplate", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("NodeTemplateId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("NodeTemplateId");

                    b.ToTable("FieldTemplates");

                    b.HasData(
                        new { Id = 1L, Name = "Name", NodeTemplateId = 1L, Type = "System.String" },
                        new { Id = 2L, Name = "Url", NodeTemplateId = 1L, Type = "System.String" },
                        new { Id = 3L, Name = "Field", NodeTemplateId = 1L, Type = "System.String" },
                        new { Id = 4L, Name = "Name", NodeTemplateId = 2L, Type = "System.String" },
                        new { Id = 5L, Name = "Url", NodeTemplateId = 2L, Type = "System.String" },
                        new { Id = 6L, Name = "Content", NodeTemplateId = 2L, Type = "System.String" },
                        new { Id = 7L, Name = "Content-Type", NodeTemplateId = 2L, Type = "System.String" },
                        new { Id = 8L, Name = "Name", NodeTemplateId = 3L, Type = "System.String" },
                        new { Id = 9L, Name = "Url", NodeTemplateId = 3L, Type = "System.String" },
                        new { Id = 10L, Name = "Content", NodeTemplateId = 3L, Type = "System.String" },
                        new { Id = 11L, Name = "Content-Type", NodeTemplateId = 3L, Type = "System.String" }
                    );
                });

            modelBuilder.Entity("Asdf.Domain.Templates.NodeTemplate", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivatorAssembly");

                    b.Property<string>("ActivatorType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NodeTemplates");

                    b.HasData(
                        new { Id = 1L, ActivatorAssembly = "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivatorType = "Asdf.Domain.Actions.HttpGetNode", Name = "HTTP GET" },
                        new { Id = 2L, ActivatorAssembly = "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivatorType = "Asdf.Domain.Actions.HttpPostNode", Name = "HTTP POST" },
                        new { Id = 3L, ActivatorAssembly = "Asdf.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivatorType = "Asdf.Domain.Actions.HttpGetNode", Name = "ATTRIBUTE" }
                    );
                });

            modelBuilder.Entity("Asdf.Domain.Triggers.Trigger", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Context");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<long?>("RootId");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("Triggers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Trigger");
                });

            modelBuilder.Entity("Asdf.Domain.Users.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<long?>("AuthId");

                    b.Property<string>("AuthProvider");

                    b.Property<string>("Email");

                    b.Property<string>("JwtToken");

                    b.Property<string>("Name");

                    b.Property<int?>("Pin");

                    b.Property<Guid?>("Token");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.AttributeNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Attributes");

                    b.ToTable("AttributeNode");

                    b.HasDiscriminator().HasValue("AttributeNode");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.DecisionNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Left");

                    b.Property<int>("Operation");

                    b.Property<string>("Right");

                    b.ToTable("DecisionNode");

                    b.HasDiscriminator().HasValue("DecisionNode");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.HttpGetNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Field");

                    b.Property<string>("Url");

                    b.ToTable("HttpGetNode");

                    b.HasDiscriminator().HasValue("HttpGetNode");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.HttpPostNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Content");

                    b.Property<string>("ContentType");

                    b.Property<string>("Url")
                        .HasColumnName("HttpPostNode_Url");

                    b.ToTable("HttpPostNode");

                    b.HasDiscriminator().HasValue("HttpPostNode");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.LogNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Level");

                    b.ToTable("LogNode");

                    b.HasDiscriminator().HasValue("LogNode");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.TemplateNode", b =>
                {
                    b.HasBaseType("Asdf.Domain.Actions.Node");

                    b.Property<string>("Field")
                        .HasColumnName("TemplateNode_Field");

                    b.Property<string>("Template");

                    b.ToTable("TemplateNode");

                    b.HasDiscriminator().HasValue("TemplateNode");
                });

            modelBuilder.Entity("Asdf.Domain.Triggers.ButtonTrigger", b =>
                {
                    b.HasBaseType("Asdf.Domain.Triggers.Trigger");


                    b.ToTable("ButtonTrigger");

                    b.HasDiscriminator().HasValue("ButtonTrigger");
                });

            modelBuilder.Entity("Asdf.Domain.Actions.Node", b =>
                {
                    b.HasOne("Asdf.Domain.Actions.Node", "Fail")
                        .WithOne()
                        .HasForeignKey("Asdf.Domain.Actions.Node", "FailId");

                    b.HasOne("Asdf.Domain.Actions.Node", "Pass")
                        .WithOne()
                        .HasForeignKey("Asdf.Domain.Actions.Node", "PassId");
                });

            modelBuilder.Entity("Asdf.Domain.Devices.Device", b =>
                {
                    b.HasOne("Asdf.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Asdf.Domain.Flows.Flow", b =>
                {
                    b.HasOne("Asdf.Domain.Triggers.Trigger", "Trigger")
                        .WithMany()
                        .HasForeignKey("TriggerId");
                });

            modelBuilder.Entity("Asdf.Domain.Templates.FieldTemplate", b =>
                {
                    b.HasOne("Asdf.Domain.Templates.NodeTemplate")
                        .WithMany("Fields")
                        .HasForeignKey("NodeTemplateId");
                });

            modelBuilder.Entity("Asdf.Domain.Triggers.Trigger", b =>
                {
                    b.HasOne("Asdf.Domain.Actions.Node", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");
                });
#pragma warning restore 612, 618
        }
    }
}
