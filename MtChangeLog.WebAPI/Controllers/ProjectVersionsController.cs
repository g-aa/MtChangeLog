using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;
using MtChangeLog.DataObjects.Enumerations;

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
        }

        // GET: api/<ProjectsVersionsController>
        [HttpGet]
        public IEnumerable<DataObjects.Entities.Views.ProjectVersionView> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation($"HTTP GET - all ProjectVersions");
            return result;
        }

        // GET api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - ProjectVersion by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP GET - ProjectVersion: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectVersion: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectsVersionsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(ProjectVersionEditable.Default);
        }

        // GET api/<ProjectsVersionsController>/statuses
        [HttpGet("statuses")]
        public IActionResult GetProjectStatuses() 
        {
            return this.Ok(Enum.GetNames(typeof(Status)));
        }

        // POST api/<ProjectsVersionsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectVersionEditable entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new ProjectVersion {entity}");
                return this.Ok($"The project version {entity} addig to the database");
            }
            catch(ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP POST - new ProjectVersion: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new ProjectVersion: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectVersionEditable entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - ProjectVersion by id = {id}");
                return this.Ok($"Project version {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ProjectVersion: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ProjectVersion: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - ProjectVersion by id = {id}");
                return this.Ok($"The ProjectVersion id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ProjectVersion: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
