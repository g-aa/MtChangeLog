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
    public class RelayAlgorithmsController : ControllerBase
    {
        private readonly IRelayAlgorithmsRepository repository;
        private readonly ILogger logger;

        public RelayAlgorithmsController(IRelayAlgorithmsRepository repository, ILogger<RelayAlgorithmsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - RelayAlgorithmsController - creating");
        }

        // GET: api/<RelayAlgorithmsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - RelayAlgorithmsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - RelayAlgorithmsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<RelayAlgorithmsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - RelayAlgorithmsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - RelayAlgorithmsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<RelayAlgorithmsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - RelayAlgorithmsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - RelayAlgorithmsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - RelayAlgorithmsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - RelayAlgorithmsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - RelayAlgorithmsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<RelayAlgorithmsController>
        [HttpPost]
        public IActionResult Post([FromBody] RelayAlgorithmEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - RelayAlgorithmEditable - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Relay algorithm {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new RelayAlgorithmEditable - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new RelayAlgorithmEditable - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] RelayAlgorithmEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - RelayAlgorithmEditable - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Relay algorithm {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - RelayAlgorithmEditable - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - RelayAlgorithmEditable - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - RelayAlgorithmsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The RelayAlgorithm id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - RelayAlgorithmsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
