using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Abstractions.Services;
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
        private readonly IProjectTreesService service;
        private readonly ILogger logger;

        public ProjectTreesController(IProjectTreesService service, ILogger<ProjectRevisionsController> logger) 
        {
            this.service = service;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectTreesController - creating");
        }

        // GET: api/<ProjectsTreesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectTreesController - projects titles");
                var result = this.service.GetProjectTitles();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectTreesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectsTreesController>/КСЗ
        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectTreesController - {title} projects trees");
                var result = this.service.GetTreeEntities(title);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectTreesController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
