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
    public class ArmEditsController : ControllerBase
    {
        private readonly IArmEditsRepository repository;
        private readonly ILogger logger;

        public ArmEditsController(IArmEditsRepository repository, ILogger<ArmEditsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ArmEditsController - creating");
        }

        // GET: api/<ArmEditsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ArmEditsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<ArmEditsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ArmEditsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ArmEditsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - ArmEditsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ArmEditsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ArmEditsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<ArmEditsController>
        [HttpPost]
        public IActionResult Post([FromBody] ArmEditEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - ArmEditsController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"ArmEdit {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - ArmEditsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ArmEditEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - ArmEditsController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"ArmEdit {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ArmEditsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - ArmEditsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The ArmEdit id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ArmEditsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
