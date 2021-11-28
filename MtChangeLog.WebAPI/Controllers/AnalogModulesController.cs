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
    public class AnalogModulesController : ControllerBase
    {
        private readonly IAnalogModulesRepository repository;
        private readonly ILogger logger;

        public AnalogModulesController(IAnalogModulesRepository repository, ILogger<AnalogModulesController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - AnalogModulesController - creating");
        }

        // GET: api/<AnalogModulesController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AnalogModulesController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<AnalogModulesController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AnalogModulesController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AnalogModulesController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AnalogModulesController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - AnalogModulesController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - AnalogModulesController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP GET - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<AnalogModulesController>
        [HttpPost]
        public IActionResult Post([FromBody] AnalogModuleEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - AnalogModulesController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Analog module {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - AnalogModulesController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP POST - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AnalogModuleEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - AnalogModulesController - entity by id = {id}");
                if (id != entity.Id) 
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Analog module {entity} update in the database");
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP PUT - AnalogModule: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - AnalogModule: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - AnalogModulesController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The analog module id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - AnalogModulesController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
