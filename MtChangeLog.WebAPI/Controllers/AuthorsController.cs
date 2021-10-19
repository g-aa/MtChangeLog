﻿using Microsoft.AspNetCore.Mvc;
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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository repository;
        private readonly ILogger logger;

        public AuthorsController(IAuthorsRepository repository, ILogger<AuthorsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public IEnumerable<AuthorBase> Get()
        {
            var result = this.repository.GetEntities();
            this.logger.LogInformation($"HTTP GET - all Authors");
            return result;
        }

        // GET api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.repository.GetEntity(id);
                this.logger.LogInformation($"HTTP GET - Author by id = {id}");
                return this.Ok(result);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP GET - Author: ");
                return this.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - Author: ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<AuthorsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(AuthorBase.Default);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthorBase entity)
        {
            try
            {
                this.repository.AddEntity(entity);
                this.logger.LogInformation($"HTTP POST - new Author {entity}");
                return this.Ok($"Author {entity} adding to the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP POST - new Author: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP POST - new Author: ");
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AuthorBase entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to entity id = {entity.Id}");
                }
                this.repository.UpdateEntity(entity);
                this.logger.LogInformation($"HTTP PUT - Author by id = {id}");
                return this.Ok($"Author {entity} update in the database");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, $"HTTP PUT - Author: ");
                return this.Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP PUT - Author: ");
                return this.BadRequest(ex.Message);
            }
        }

        // DELETE api/<AuthorsController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.repository.DeleteEntity(id);
                this.logger.LogInformation($"HTTP DELETE - Author by id = {id}");
                return this.Ok($"The Author id = {id} has been successfully removed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP DELETE - Author: ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
