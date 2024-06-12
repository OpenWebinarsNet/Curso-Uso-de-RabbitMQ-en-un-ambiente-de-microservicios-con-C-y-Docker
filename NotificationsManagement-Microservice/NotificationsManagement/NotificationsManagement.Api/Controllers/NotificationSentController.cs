using Microsoft.AspNetCore.Mvc;
using NotificationsManagement.Application.Interfaces;
using NotificationsManagement.Domain.Models;
using NotificationsManagement.Util;
using System;
using System.Threading.Tasks;

namespace NotificationsManagement.Api.Controllers
{
    [ApiController]
    [Route(Constants.Routes.NotificationsSent)]
    public class NotificationsSentController : ControllerBase
    {
        private readonly INotificationsSentApplication _notificationsSentApplication;

        public NotificationsSentController(INotificationsSentApplication notificationsSentApplication)
        {
            _notificationsSentApplication = notificationsSentApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationsSentApplication.GetAsync();

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _notificationsSentApplication.GetByIdAsync(id);

            if (result == null)
                return NotFound(Constants.ReturnMessages.NotFound);

            return Ok(result.Object);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotificationSentInputModel notificationSentInputModel)
        {
            var result = await _notificationsSentApplication.PostAsync(notificationSentInputModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] NotificationSentModel notificationSentModel)
        {
            var result = await _notificationsSentApplication.PutAsync(notificationSentModel);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _notificationsSentApplication.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Notifications);

            return Ok(result);
        }
    }
}