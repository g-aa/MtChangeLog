using Microsoft.AspNetCore.Mvc;

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
    public class AnalogModulesController : ControllerBase
    {
        private readonly IAnalogModuleRepository moduleRepository;

        public AnalogModulesController(IAnalogModuleRepository moduleRepository) 
        {
            this.moduleRepository = moduleRepository;
        }

        // GET: api/<AnalogModulesController>
        [HttpGet]
        public IEnumerable<AnalogModuleBase> Get()
        {
            var result = this.moduleRepository.GetEntities();
            return result;
        }

        // GET api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = this.moduleRepository.GetEntity(id);
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

        // GET api/<AnalogModulesController>/default
        [HttpGet("default")]
        public IActionResult GetDefault() 
        {
            return this.Ok(AnalogModuleEditable.Default);
        }

        // POST api/<AnalogModulesController>
        [HttpPost]
        public IActionResult Post([FromBody] AnalogModuleEditable module)
        {
            try
            {
                this.moduleRepository.AddEntity(module);
                return this.Ok($"Analog module {module.DIVG} {module.Title} adding to the database");
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

        // PUT api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] AnalogModuleEditable module)
        {
            try
            {
                if (id != module.Id) 
                {
                    throw new ArgumentException($"url id = {id} is not equal to model id = {module.Id}");
                }
                this.moduleRepository.UpdateEntity(module);
                return this.Ok($"Analog module {module.DIVG} {module.Title} update in the database");
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

        // DELETE api/<AnalogModulesController>/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this.moduleRepository.DeleteEntity(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
