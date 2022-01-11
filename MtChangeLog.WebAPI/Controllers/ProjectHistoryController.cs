using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectHistoryController : ControllerBase
    {
        private readonly IStatisticsRepository statisticsRepository;
        private readonly ILogger logger;

        public ProjectHistoryController(IStatisticsRepository statisticsRepository, ILogger<ProjectHistoryController> logger)
        {
            this.statisticsRepository = statisticsRepository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectHistoryController - creating");
        }

        // GET: api/<ProjectHistoryController>
        [HttpGet]
        public IActionResult GetProjectVersions()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectHistoryController - all short entities");
                var result = this.statisticsRepository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectHistoryController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/<ProjectHistoryController>/Version/00000000-0000-0000-0000-000000000000
        [HttpGet("Version/{id}")]
        public IActionResult GetProjectVersionHistory(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectHistoryController - full history by project version id = {id}");
                var result = this.statisticsRepository.GetProjectVersionHistory(id);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectHistoryController - ");
                return this.BadRequest(ex.Message);
            }
        }

        // GET: api/<StatisticsController>/Revision/00000000-0000-0000-0000-000000000000
        [HttpGet("Revision/{id}")]
        public IActionResult GetProjectRevisionHistory(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - StatisticsController - history by project revision id = {id}");
                var result = this.statisticsRepository.GetProjectRevisionHistory(id);
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
