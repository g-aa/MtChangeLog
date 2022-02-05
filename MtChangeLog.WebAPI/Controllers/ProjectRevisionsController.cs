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
    public class ProjectRevisionsController : ControllerBase
    {
        private readonly IProjectRevisionsRepository repository;
        private readonly ILogger logger;

        public ProjectRevisionsController(IProjectRevisionsRepository repository, ILogger<ProjectRevisionsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectRevisionsСontroller - creating");
        }

        // GET: api/<ProjectRevisionsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevisionsСontroller - all short entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProjectRevisionsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevisionsСontroller - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProjectRevisionsController>/Template/00000000-0000-0000-0000-000000000000
        [HttpGet("Template/{id}")]
        public IActionResult GetTemplate(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevisionsСontroller - template by project version id = {id}");
                var result = this.repository.GetTemplate(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectRevisionsСontroller - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevisionsСontroller - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectRevisionsСontroller - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST: api/<ProjectRevisionsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectRevisionEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - ProjectRevisionsСontroller - entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Project revision {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectRevisionEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - ProjectRevisionsСontroller - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Project revision {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ProjectRevisionsСontroller - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The ProjectRevision id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProjectRevisionsСontroller - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
