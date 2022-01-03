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
    public class CommunicationModulesController : ControllerBase
    {
        private readonly ICommunicationModulesRepository repository;
        private readonly ILogger logger;

        public CommunicationModulesController(ICommunicationModulesRepository repository, ILogger<CommunicationModulesController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - CommunicationsController - creating");
        }

        // GET: api/<CommunicationsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - CommunicationsController - all short entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<CommunicationsController>/ShortViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - CommunicationsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AnalogModulesController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - CommunicationsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - CommunicationsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - CommunicationsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<CommunicationsController>
        [HttpPost]
        public IActionResult Post([FromBody] CommunicationModuleEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - CommunicationsController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Communications {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - CommunicationsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CommunicationModuleEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - CommunicationsController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Communications {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - CommunicationsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - CommunicationsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The Communication id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - CommunicationsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
