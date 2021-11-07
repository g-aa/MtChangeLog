using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTreesController : ControllerBase
    {
        private readonly IProjectRevisionsRepository repository;
        private readonly ILogger logger;

        public ProjectTreesController(IProjectRevisionsRepository repository, ILogger<ProjectRevisionsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - creating ProjectTreesController");
        }

        // GET: api/<ProjectsTreesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            this.logger.LogInformation($"HTTP GET - all projects types");
            var result = this.repository.GetProjectTypes();
            return result;
        }

        // GET api/<ProjectsTreesController>/КСЗ
        [HttpGet("{type}")]
        public IActionResult Get(string type)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - projects trees {type}");
                var result = this.repository.GetTreeEntities(type);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectTrees: ");
                return this.BadRequest(ex.Message);

            }
        }
    }
}
