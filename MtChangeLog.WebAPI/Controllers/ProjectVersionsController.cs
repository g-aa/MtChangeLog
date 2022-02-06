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
    public class ProjectVersionsController : ControllerBase
    {
        private readonly IProjectVersionsRepository repository;
        private readonly ILogger logger;

        public ProjectVersionsController(IProjectVersionsRepository repository, ILogger<ProjectVersionsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectVersionsController - creating");
        }

        // GET: api/<ProjectsVersionsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProjectsVersionsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectsVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProjectsVersionsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectVersionsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectsVersionsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ProjectVersionsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectVersionsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectVersionsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<ProjectsVersionsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectVersionEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - ProjectVersionsController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"The project version {entity} addig to the database");
            }
            catch(ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP POST - ProjectVersionsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectVersionEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - ProjectVersionsController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Project version {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProjectVersionsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ProjectVersionsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The ProjectVersion id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProjectVersionsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
