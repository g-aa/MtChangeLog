﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformsRepository repository;
        private readonly ILogger logger;

        public PlatformsController(IPlatformsRepository platformsRepository, ILogger<PlatformsController> logger) 
        { 
            this.repository = platformsRepository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - PlatformsController - creatin");
        }

        // GET: api/<PlatformsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - PlatformsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<PlatformsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - PlatformsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<PlatformsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - PlatformsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - PlatformsController - entity by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - PlatformsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP GET - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<PlatformsController>
        [HttpPost]
        public IActionResult Post([FromBody] PlatformEditable entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - PlatformsController - new entity {entity}");
                return this.Ok($"Platform {entity} addig to the database");
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP POST - PlatformsController - ");
                return this.Conflict(ex.Message);   
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PlatformEditable entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - PlatformsController - entity by id = {id}");
                return this.Ok($"Platform {entity} update in the database");
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP PUT - PlatformsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - PlatformsController - entity by id = {id}");
                return this.Ok($"The Platform id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - PlatformsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
