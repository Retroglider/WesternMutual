using Microsoft.AspNetCore.Mvc;

using System;

using WesternMutual_RhyssLeary.Models;
using WesternMutual_RhyssLeary.Services;

using static WesternMutual_RhyssLeary.Models.Entry;

namespace WesternMutual_RhyssLeary.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly IGuidService GuidService;
        public GuidController(IGuidService guidService)
        {
            GuidService = guidService;
        }
        [HttpPost("{guid:Guid?}")]
        public async Task<IActionResult> Create(Guid? guid,CreateModel createModel)
        {
            try
            {
                var viewModel = await GuidService
                    .Create(guid, createModel);
                return new OkObjectResult(viewModel);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("{guid}")]
        public async Task<IActionResult> Read(Guid guid) 
        {
            try
            {
                var viewModel = await GuidService.Read(guid);

                return new OkObjectResult(viewModel);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpPatch("{guid}")] // Changed from HttpPost due to conflict with CREATE endpoint.  Plus more semantically appropriate.
        public async Task<IActionResult> Update(Guid guid,UpdateModel updateModel)
        {
            try
            {
                var viewModel = await GuidService.Update(guid,updateModel);

                return new OkObjectResult(viewModel);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                await GuidService.Delete(guid);
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
