using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtChangeLog.Abstractions.Services;
using MtChangeLog.TransferObjects.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MtChangeLog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IProjectHistoriesService service;
        private readonly ILogger logger;

        public FilesController(IProjectHistoriesService service, ILogger<FilesController> logger) 
        {
            this.service = service;
            this.logger = logger;
        }

        // GET api/<FilesController>/Export/00000000-0000-0000-0000-000000000000
        [HttpGet("ProjectHistory/{id}")]
        public IActionResult GetProjectHistoryForExport(Guid id)
        {
            try
            {
                this.logger.LogInformation($"HTTP GET - FilesController - export to file full history by project version id = {id}");
                var result = this.service.GetProjectVersionHistory(id);
                return this.Ok(new FileModel(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - FilesController - ");
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet("ProjectHistorysArchive")]
        public IActionResult GetProjectHistorysArchiveForExport() 
        {
            try
            {
                FileModel result = null;
                var projects = this.service.GetShortEntities().ToList();
                using (MemoryStream outStream = new MemoryStream()) 
                {
                    using (ZipArchive archive = new ZipArchive(outStream, ZipArchiveMode.Create, true)) 
                    {
                        foreach (var project in projects)
                        {
                            var projectFile = new FileModel(this.service.GetProjectVersionHistory(project.Id));
                            ZipArchiveEntry entry = archive.CreateEntry(projectFile.Title);
                            using (var entryStream = entry.Open()) 
                            {
                                using (MemoryStream writer = new MemoryStream(projectFile.Bytes.ToArray()))
                                {
                                    writer.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                    result = new FileModel("ChangeLog.zip", outStream.ToArray());
                }
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"HTTP GET - FilesController - ");
                return this.BadRequest(ex.Message);
            }
        }
    }
}
