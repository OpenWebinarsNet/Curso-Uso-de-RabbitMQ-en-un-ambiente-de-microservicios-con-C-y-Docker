using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkinghoursManagement.Application.Interfaces;
using WorkinghoursManagement.Domain.Interfaces;
using WorkinghoursManagement.Domain.Models;
using WorkinghoursManagement.Util;

namespace WorkingHoursManagement.Api.Jobs
{
    public class CheckUsersWorkingHoursJob
    {
        private readonly IWorkingHoursByUserApplication _workingHoursByUserApplication;
        private readonly INotificationsMessageBrokerPublisher _notificationsMessageBrokerPublisher;

        public CheckUsersWorkingHoursJob(
            IWorkingHoursByUserApplication workingHoursByUserApplication,
            INotificationsMessageBrokerPublisher notificationsMessageBrokerPublisher
            )
        {
            _workingHoursByUserApplication = workingHoursByUserApplication;
            _notificationsMessageBrokerPublisher = notificationsMessageBrokerPublisher;
        }

        public async Task DoCheckAsync(CancellationToken ct = default)
        {
            Console.WriteLine("#### CHECKING USER WORKING HOURS ####");
            var allWorkingHoursResult = await _workingHoursByUserApplication.GetAsync();
            var allWorkingHours = allWorkingHoursResult.Object;
            IEnumerable<IGrouping<Guid, WorkingHoursByUserModel>> workingHoursGroupedByUsers = allWorkingHours.GroupBy(d => d.UserId);
            foreach (var workingHoursByUser in workingHoursGroupedByUsers)
            {
                double workedMinutesByUser = 0;
                var registeredTimes = workingHoursByUser.OrderBy(d => d.RegisteredDateTime).ToList();
                for (int i = 0; i < registeredTimes.Count; i++)
                {
                    if (i + 1 == registeredTimes.Count) continue;

                    var registeredTime = registeredTimes[i];
                    var nextRegisteredTime = registeredTimes[i + 1];
                    if (registeredTime.RegisterType == Constants.RegisterConstants.RegisterType_Out) continue; //do not read if the firstone is an out

                    if (registeredTime.RegisterType == Constants.RegisterConstants.RegisterType_In && nextRegisteredTime.RegisterType == Constants.RegisterConstants.RegisterType_Out)
                    {
                        workedMinutesByUser += (nextRegisteredTime.RegisteredDateTime - registeredTime.RegisteredDateTime).TotalMinutes;
                    }
                }

                if (workedMinutesByUser > 20)
                {
                    Console.WriteLine($"User {workingHoursByUser.Key} worked {workedMinutesByUser} minutes!!. Send notification to rabbit for the notifications microservice");
                    await _notificationsMessageBrokerPublisher.PublishNotificationAsync(new NotificationSentInputModel
                    {
                        Id = Guid.NewGuid(),
                        UserId = workingHoursByUser.Key,
                        NotificationTypeId = Guid.Parse(Constants.NotificationConstants.Congrats),
                        NotificationMessage = $"User {workingHoursByUser.Key} worked {workedMinutesByUser} minutes!!",
                        NotificationDatetime = DateTime.Now
                    });
                }
            }
        }
    }
}