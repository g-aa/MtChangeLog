using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService service;
        private readonly ILogger logger;

        public StatisticsController(IStatisticsService service, ILogger<StatisticsController> logger) 
        {
            this.service = service;
            this.logger = logger;
            this.logger.LogInformation("HTTP - StatisticsController - creating");
        }

        // GET: api/<StatisticsController>
        [HttpGet("Short")]
        public IActionResult Get()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - StatisticsController - project short statistics");
                var result = this.service.GetStatistics();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - StatisticsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<StatisticsController>/LastProjectsRevision
        [HttpGet("LastProjectsRevision")]
        public IActionResult GetLastProjectsRevision() 
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - StatisticsController - last projects revision");
                var result = this.service.GetLastProjectRevisions();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - StatisticsController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<StatisticsController>/AuthorProjectContributions
        [HttpGet("AuthorProjectContributions")]
        public IActionResult GetAuthorProjectContributions() 
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - StatisticsController - author project contributions");
                var result = this.service.GetAuthorProjectContributions();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - StatisticsController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
