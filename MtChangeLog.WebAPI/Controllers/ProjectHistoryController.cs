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
        private readonly IProjectVersionsRepository versionsRepository;
        private readonly IProjectRevisionsRepository revisionsRepository;
        private readonly ILogger logger;

        public ProjectHistoryController(IProjectVersionsRepository versionsRepository, IProjectRevisionsRepository revisionsRepository, ILogger<ProjectHistoryController> logger)
        {
            this.versionsRepository = versionsRepository;
            this.revisionsRepository = revisionsRepository;
            this.logger = logger;
            this.logger.LogInformation("HTTP - ProjectHistoryController - creating");
        }

        // GET: api/<ProjectHistoryController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectHistoryController - all short entities");
                var result = this.versionsRepository.GetShortEntities();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectHistoryController - ");
                return this.BadRequest(ex.Message);
                throw;
            }
        }

        // GET api/<ProjectHistoryController>/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - ProjectHistoryController - full history by project id = {id}");
                var result = this.revisionsRepository.GetProjectHistories(id);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - ProjectHistoryController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
