using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ProtocolsController : ControllerBase
    {
        private readonly IProtocolsRepository repository;
        private readonly ILogger logger;

        public ProtocolsController(IProtocolsRepository repository, ILogger<ProtocolsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProtocolsController - creating");
        }

        // GET: api/<ProtocolsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProtocolsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProtocolsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProtocolsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProtocolsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProtocolsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProtocolsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProtocolsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProtocolsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<ProtocolsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProtocolEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - ProtocolsController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"The protocol {entity} addig to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - ProtocolsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProtocolsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProtocolEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - ProtocolsController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"The protocol {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProtocolsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProtocolsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ProtocolsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The protocol id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProtocolsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
