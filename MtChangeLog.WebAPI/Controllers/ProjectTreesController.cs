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
        private readonly IProjectVersionsRepository versionsRepository;
        private readonly IProjectRevisionsRepository revisionsRepository;
        private readonly ILogger logger;

        public ProjectTreesController(IProjectVersionsRepository versionsRepository, IProjectRevisionsRepository revisionsRepository, ILogger<ProjectRevisionsController> logger) 
        {
            this.versionsRepository = versionsRepository;
            this.revisionsRepository = revisionsRepository;
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
                var result = this.versionsRepository.GetProjectTitles();
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
                var result = this.revisionsRepository.GetTreeEntities(title);
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
