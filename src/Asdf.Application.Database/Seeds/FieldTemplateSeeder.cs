﻿using System;
using Asdf.Domain.Actions;
using Asdf.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Database.Seeds
{
    public static class FieldTemplateSeeder
    {
        public static ModelBuilder SeedFieldTemplates(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<FieldTemplate>()
                .HasData(
                    new FieldTemplate() {Id = 1, NodeTemplateId = 1, Name = "Name", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 2, NodeTemplateId = 1, Name = "Url", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 3, NodeTemplateId = 1, Name = "Field", Type = typeof(String).FullName},

                    new FieldTemplate() {Id = 4, NodeTemplateId = 2, Name = "Name", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 5, NodeTemplateId = 2, Name = "Url", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 6, NodeTemplateId = 2, Name = "Content", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 7, NodeTemplateId = 2, Name = "Content-Type", Type = typeof(String).FullName},

                    new FieldTemplate() {Id = 8, NodeTemplateId = 3, Name = "Name", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 9, NodeTemplateId = 3, Name = "Key", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 10, NodeTemplateId = 3, Name = "Value", Type = typeof(String).FullName},
                    new FieldTemplate() {Id = 11, NodeTemplateId = 3, Name = "Type", Type = typeof(String).FullName},

                    new FieldTemplate() { Id = 12, NodeTemplateId = 4, Name = "Name", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 13, NodeTemplateId = 4, Name = "Device", Type = typeof(Guid).FullName },
                    new FieldTemplate() { Id = 14, NodeTemplateId = 4, Name = "Field", Type = typeof(String).FullName },

                    new FieldTemplate() { Id = 15, NodeTemplateId = 5, Name = "Name", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 16, NodeTemplateId = 5, Name = "Left Field", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 17, NodeTemplateId = 5, Name = "Operation", Type = typeof(OperationType).FullName },
                    new FieldTemplate() { Id = 18, NodeTemplateId = 5, Name = "Right Field", Type = typeof(String).FullName },

                    new FieldTemplate() { Id = 19, NodeTemplateId = 6, Name = "Name", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 20, NodeTemplateId = 6, Name = "Gpio", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 21, NodeTemplateId = 6, Name = "Status", Type = typeof(GpioStatusType).FullName },

                    new FieldTemplate() { Id = 22, NodeTemplateId = 7, Name = "Name", Type = typeof(String).FullName },
                    new FieldTemplate() { Id = 23, NodeTemplateId = 7, Name = "Field", Type = typeof(String).FullName }

            );

            return modelBuilder;
        }
    }
}
