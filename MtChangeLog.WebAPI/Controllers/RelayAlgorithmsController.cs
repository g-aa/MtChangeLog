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
    public class RelayAlgorithmsController : ControllerBase
    {
        private readonly IRelayAlgorithmsRepository repository;
        private readonly ILogger logger;

        public RelayAlgorithmsController(IRelayAlgorithmsRepository repository, ILogger<RelayAlgorithmsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
        }
        
        // GET: api/<RelayAlgorithmsController>
        [HttpGet]
        public IEnumerable<RelayAlgorithmBase> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation($"HTTP GET - all RelayAlgorithms");
            return result;
        }

        // GET api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - RelayAlgorithm by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - RelayAlgorithm: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - RelayAlgorithm: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<RelayAlgorithmsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(RelayAlgorithmBase.Default);
        }

        // POST api/<RelayAlgorithmsController>
        [HttpPost]
        public IActionResult Post([FromBody] RelayAlgorithmBase entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new RelayAlgorithm {entity}");
                return this.Ok($"Relay algorithm {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new RelayAlgorithm: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new RelayAlgorithm: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] RelayAlgorithmBase entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - RelayAlgorithm by id = {id}");
                return this.Ok($"Relay algorithm {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - RelayAlgorithm: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - RelayAlgorithm: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - RelayAlgorithm by id = {id}");
                return this.Ok($"The RelayAlgorithm id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - RelayAlgorithm: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
