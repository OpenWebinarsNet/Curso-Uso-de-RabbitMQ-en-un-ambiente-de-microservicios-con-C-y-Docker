using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsManagement.Domain.Interfaces
{
    public interface IMessageBrokerPublisher
    {
        void CreateExchange();
    }
}
