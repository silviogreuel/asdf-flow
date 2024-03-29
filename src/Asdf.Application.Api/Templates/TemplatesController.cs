﻿using System.Linq;
using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Asdf.Application.Database;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Api.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : AuthorizeController
    {
        private readonly AsdfContext db;

        public TemplatesController(AsdfContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ListTemplates()
        {
            var templates = await db.NodeTemplates.ToListAsync();
            foreach (var template in templates)
            {
                template.Fields = template.Fields.OrderBy(f => f.Id).ToList();
            }

            return Ok(new
            {
                templates
            });
        }
    }
}