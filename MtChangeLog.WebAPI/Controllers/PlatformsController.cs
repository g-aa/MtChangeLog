using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Entities;
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
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformsRepository platforms;
        private readonly ILogger logger;

        public PlatformsController(IPlatformsRepository platformsRepository, ILogger<PlatformsController> logger) 
        { 
            this.platforms = platformsRepository;
            this.logger = logger;
        }
        
        // GET: api/<PlatformsController>
        [HttpGet]
        public IEnumerable<PlatformBase> Get()
        {
            var result = this.platforms.GetEntities();
            this.logger.LogInformation($"HTTP GET - all Platforms");
            return result;
        }

        // GET api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.platforms.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - Platform by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - Platform: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, $"HTTP GET - Platform: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<PlatformsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault() 
        {
            return this.Ok(PlatformEditable.Default);
        }

        // POST api/<PlatformsController>
        [HttpPost]
        public IActionResult Post([FromBody] PlatformEditable entity)
        {
            try
            {
                this.platforms.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new Platform {entity}");
                return this.Ok($"Platform {entity} addig to the database");
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP POST - new Platform: ");
                return this.Conflict(ex.Message);   
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new Platform: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PlatformEditable entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.platforms.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - Platform by id = {id}");
                return this.Ok($"Platform {entity} update in the database");
            }
            catch (ArgumentException ex) 
            {
                this.logger.LogWarning(ex, $"HTTP PUT - Platform: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - Platform: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.platforms.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - Platform by id = {id}");
                return this.Ok($"The Platform id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - Platform: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
