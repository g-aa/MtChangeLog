using Microsoft.AspNetCore.Mvc;

using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Enumerations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectVersionsController : ControllerBase
    {
        private readonly IProjectVersionsRepository repository;

        public ProjectVersionsController(IProjectVersionsRepository repository) 
        {
            this.repository = repository;
        }

        // GET: api/<ProjectsVersionsController>
        [HttpGet]
        public IEnumerable<ProjectVersionBase> Get()
        {
            var result = this.repository.GetEntities();
            return result;
        }

        // GET api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
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
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectsVersionsController>/default
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return this.Ok(ProjectVersionEditable.Default);
        }

        // GET api/<ProjectsVersionsController>/statuses
        [HttpGet("statuses")]
        public IActionResult GetProjectStatuses() 
        {
            return this.Ok(Enum.GetNames(typeof(Status)));
        }

        // POST api/<ProjectsVersionsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjectVersionEditable projectVersion)
        {
            try
            {
                this.repository.AddEntity(projectVersion);
                return this.Ok($"The project version {projectVersion.DIVG}, {projectVersion.Title} addig to the database");
            }
            catch(ArgumentException ex) 
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // PUT api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProjectVersionEditable projectVersion)
        {
            try
            {
                if (id != projectVersion.Id)
                {
                    throw new ArgumentException($"url id = {id} is not equal to project version id = {projectVersion.Id}");
                }
                this.repository.UpdateEntity(projectVersion);
                return this.Ok($"Project version {projectVersion.DIVG}, {projectVersion.Title} update in the database");
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

        // DELETE api/<ProjectsVersionsController>/00000000-0000-0000-0000-000000000000
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
