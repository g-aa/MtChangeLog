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
    public class ArmEditsController : ControllerBase
    {
        private readonly IArmEditsRepository repository;
        private readonly ILogger logger;

        public ArmEditsController(IArmEditsRepository repository, ILogger<ArmEditsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET: api/<ArmEditsController>
        [HttpGet]
        public IEnumerable<ArmEditBase> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation($"HTTP GET - all ArmEdits");
            return result;
        }

        // GET api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - ArmEdit by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - ArmEdit: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ArmEdit: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ArmEditsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(ArmEditBase.Default);
        }

        // POST api/<ArmEditsController>
        [HttpPost]
        public IActionResult Post([FromBody] ArmEditBase entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new ArmEdit {entity}");
                return this.Ok($"ArmEdit {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new ArmEdit: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new ArmEdit: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ArmEditBase entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - ArmEdit by id = {id}");
                return this.Ok($"ArmEdit {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - ArmEdit: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - ArmEdit: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - ArmEdit by id = {id}");
                return this.Ok($"The ArmEdit id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ArmEdit: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
