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
        private readonly IPlatformsRepository platformsRepository;
        private readonly ILogger logger;

        public PlatformsController(IPlatformsRepository platformsRepository, ILogger<PlatformsController> logger) 
        { 
            this.platformsRepository = platformsRepository;
            this.logger = logger;
        }
        
        // GET: api/<PlatformsController>
        [HttpGet]
        public IEnumerable<PlatformBase> Get()
        {
            var result = this.platformsRepository.GetEntities();
            return result;
        }

        // GET api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.platformsRepository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
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
        public IActionResult Post([FromBody] PlatformEditable platform)
        {
            try
            {
                this.platformsRepository.AddEntity(platform);
                return this.Ok($"The platform {platform.Title} addig to the database");
            }
            catch (ArgumentException ex) 
            {
                return this.BadRequest(ex.Message);   
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PlatformEditable platform)
        {
            try
            {
                if (id != platform.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to platform id = {platform.Id}");
                }
                this.platformsRepository.UpdateEntity(platform);
                return this.Ok($"Platform {platform.Title} update in the database");
            }
            catch (ArgumentException ex) 
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<PlatformsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.platformsRepository.DeleteEntity(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
