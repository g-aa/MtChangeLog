using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.DataBase.Repositories.Interfaces;
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
        private readonly IStatisticsRepository repository;
        private readonly ILogger logger;

        public StatisticsController(IStatisticsRepository repository, ILogger<StatisticsController> logger) 
        {
            this.repository = repository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - StatisticsController - creating");
        }

        // GET: api/<StatisticsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - StatisticsController - project short statistics");
                var result = this.repository.GetShortStatistics();
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
