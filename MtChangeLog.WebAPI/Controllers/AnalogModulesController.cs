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
    public class AnalogModulesController : ControllerBase
    {
        private readonly IAnalogModulesRepository repository;
        private readonly ILogger logger;

        public AnalogModulesController(IAnalogModulesRepository repository, ILogger<AnalogModulesController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET: api/<AnalogModulesController>
        [HttpGet]
        public IEnumerable<AnalogModuleBase> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation("HTTP GET - all AnalogModules");
            return result;
        }

        // GET api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - AnalogModule by id = {id}"); 
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - AnalogModule: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP GET - AnalogModule: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AnalogModulesController>/default
        [HttpGet("default")]
        public IActionResult GetDefault() 
        {
            return this.Ok(AnalogModuleEditable.Default);
        }

        // POST api/<AnalogModulesController>
        [HttpPost]
        public IActionResult Post([FromBody] AnalogModuleEditable entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new AnalogModule {entity}");
                return this.Ok($"Analog module {entity.DIVG} {entity.Title} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new AnalogModule: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP POST - new AnalogModule: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AnalogModuleEditable entity)
        {
            try
            {
                if (id != entity.Id) 
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - AnalogModule by id = {id}");
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
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - AnalogModule by id = {id}");
                return this.Ok($"The analog module id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - ArmEdit: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
