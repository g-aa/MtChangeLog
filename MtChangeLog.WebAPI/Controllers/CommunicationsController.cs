using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
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
    public class CommunicationsController : ControllerBase
    {
        private readonly ICommunicationsRepository repository;
        private readonly ILogger logger;

        public CommunicationsController(ICommunicationsRepository repository, ILogger<CommunicationsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET: api/<CommunicationsController>
        [HttpGet]
        public IEnumerable<CommunicationBase> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation($"HTTP GET - all Communications");
            return result;
        }

        // GET api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - Communication by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - Communication: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - Communication: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<CommunicationsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(CommunicationBase.Default);
        }

        // POST api/<CommunicationsController>
        [HttpPost]
        public IActionResult Post([FromBody] CommunicationBase entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new Communication {entity}");
                return this.Ok($"Communications {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new Communication: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new Communication: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CommunicationBase entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - Communication by id = {id}");
                return this.Ok($"Communications {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - Communication: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - Communication: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<CommunicationsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - Communication by id = {id}");
                return this.Ok($"The Communication id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - Communication: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
