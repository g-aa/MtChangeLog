using Microsoft.AspNetCore.Mvc;

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
        private readonly IArmEditRepository repository;

        public ArmEditsController(IArmEditRepository repository) 
        {
            this.repository = repository;
        }

        // GET: api/<ArmEditsController>
        [HttpGet]
        public IEnumerable<ArmEditBase> Get()
        {
            var result = this.repository.GetEntities();
            return result;
        }

        // GET api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
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
                return this.Ok($"ArmEdit {entity.DIVG} {entity.Version} adding to the database");
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
                return this.Ok($"ArmEdit {entity.DIVG} {entity.Version} update in the database");
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

        // DELETE api/<ArmEditsController>/00000000-0000-0000-0000-000000000000
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
