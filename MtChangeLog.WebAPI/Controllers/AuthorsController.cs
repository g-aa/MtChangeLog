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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository repository;
        private readonly ILogger logger;

        public AuthorsController(IAuthorsRepository repository, ILogger<AuthorsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - AuthorsController - creating");

        }

        // GET: api/<AuthorsController>/ShortViews
        [HttpGet("ShortViews")]
        public IActionResult GetShortViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AuthorsController - all sorts entities");
                var result = this.repository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<AuthorsController>/TableViews
        [HttpGet("TableViews")]
        public IActionResult GetTableViews()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AuthorsController - all entities for table");
                var result = this.repository.GetTableEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AuthorsController>/Template
        [HttpGet("Template")]
        public IActionResult GetTemplate()
        {
            try
            {
                this.logger.LogInformation("HTTP GET - AuthorsController - template");
                var result = this.repository.GetTemplate();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "HTTP GET - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - AuthorsController - entity by id = {id}");
                var result = this.repository.GetEntity(id);
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - AuthorsController - ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthorEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP POST - AuthorsController - new entity {entity}");
                this.repository.AddEntity(entity);
                return this.Ok($"Author {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - AuthorsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AuthorEditable entity)
        {
            try
            {
                this.logger.LogInformation($"HTTP PUT - AuthorsController - entity by id = {id}");
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                return this.Ok($"Author {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - AuthorsController - ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP DELETE - AuthorsController - entity by id = {id}");
                this.repository.DeleteEntity(id);
                return this.Ok($"The Author id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - AuthorsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
