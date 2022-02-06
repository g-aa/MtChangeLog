using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusesController : ControllerBase
    {
        private readonly IProjectStatusRepository repository;
        private readonly ILogger logger;

        public ProjectStatusesController(IProjectStatusRepository repository, ILogger<ProjectStatusesController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectStatusesController - creating");
        }

        // GET: api/<ProjectStatusesController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProjectStatusesController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProjectStatusesController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProjectStatusesController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectStatusesController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProjectStatusesController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectStatusesController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectStatusesController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectStatusesController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<ProjectStatusesController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectStatusEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - ProjectStatusesController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"The project status {entity} addig to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - ProjectStatusesController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectStatusesController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectStatusEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - ProjectStatusesController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Project status {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProjectStatusesController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProjectStatusesController>/0000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ProjectStatusesController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The project status id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProjectStatusesController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
