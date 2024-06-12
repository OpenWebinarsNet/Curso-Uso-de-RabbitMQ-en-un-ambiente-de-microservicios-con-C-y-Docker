using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkinghoursManagement.Application.Interfaces;
using WorkinghoursManagement.Domain.Models;
using WorkinghoursManagement.Util;

namespace WorkinghoursManagement.Api.Controllers
{
    [ApiController]
    [Route(Constants.Routes.WorkingHours)]
    public class WorkingHoursByUserController : ControllerBase
    {
        private readonly IWorkingHoursByUserApplication _workingHoursByUserApplication;

        public WorkingHoursByUserController(IWorkingHoursByUserApplication workingHoursByUserApplication)
        {
            _workingHoursByUserApplication = workingHoursByUserApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _workingHoursByUserApplication.GetAsync();

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _workingHoursByUserApplication.GetByIdAsync(id);

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkingHoursByUserInputModel workingHoursByUserInputModel)
        {
            var result = await _workingHoursByUserApplication.PostAsync(workingHoursByUserInputModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkingHoursByUserModel workingHoursByUserInputModel)
        {
            var result = await _workingHoursByUserApplication.PutAsync(workingHoursByUserInputModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _workingHoursByUserApplication.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }
    }
}