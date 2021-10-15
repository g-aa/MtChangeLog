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
            return result;
        }

        // GET api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
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
                return this.Ok($"Relay algorithm {entity.ANSI} {entity.Title} adding to the database");
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
                return this.Ok($"Relay algorithm {entity.ANSI} {entity.Title} update in the database");
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

        // DELETE api/<RelayAlgorithmsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
