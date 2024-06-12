using Flunt.Notifications;
using System;

namespace UsersManagement.Domain.Core.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; set; }

        protected BaseEntity() => Id = Guid.NewGuid();
    }
}