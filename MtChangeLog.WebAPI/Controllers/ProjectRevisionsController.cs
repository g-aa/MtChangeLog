using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;

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
            this.logger.LogInformation("HTTP - creating ProjectRevisionsСontroller");
        }

        // GET: api/<ProjectRevisionsController>
        [HttpGet]
        public IEnumerable<ProjectRevisionTableView> Get()
        {
            this.logger.LogInformation($"HTTP GET - all ProjectRevisions for table");
            var result = this.repository.GetTableEntities();
            return result;
        }

        // GET: api/<ProjectRevisionsController>/ShortViews
        [HttpGet("ShortViews")]
        public IEnumerable<ProjectRevisionShortView> GetShortViews() 
        {
            this.logger.LogInformation($"HTTP GET - all short view ProjectRevisions");
            var result = this.repository.GetShortEntities();
            return result;
        }

        // GET api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevision by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectRevision: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectRevisionsController>/ByProjectVersionId/00000000-0000-0000-0000-000000000000
        [HttpGet("ByProjectVersionId/{id}")]
        public IActionResult GetByProjectVersionId(Guid id) 
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectRevision by ProjectVersion id = {id}");
                var result = this.repository.GetByProjectVersionId(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectRevision: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<ProjectRevisionsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectRevisionEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - new ProjectRevision {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Project revision {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectRevisionEditable entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.logger.LogInformation($"HTTP PUT - ProjectRevision by id = {id}");
                this.repository.UpdateEntity(entity);
                return this.Ok($"Project revision {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProjectRevisionsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ProjectRevision by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The ProjectRevision id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProjectRevision: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
