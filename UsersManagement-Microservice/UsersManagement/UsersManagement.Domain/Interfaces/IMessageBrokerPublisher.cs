using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Models;

namespace UsersManagement.Domain.Interfaces
{
    /// <summary>
    /// Interface for MessageBrokerPublisher
    /// </summary>
    public interface IMessageBrokerPublisher
    {
        /// <summary>
        /// Create Exchange method.
        /// </summary>
        void CreateExchange();

        /// <summary>
        /// PublishCreateUser. Sends a message to the message broker to create a user.
        /// </summary>
        /// <param name="userInputModel"></param>
        /// <returns></returns>
        void PublishCreateUser(UserInputModel userInputModel);

        /// <summary>
        /// PublishDeleteUser. Sends a message to the message broker to delete a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PublishDeleteUser(Guid id);

        /// <summary>
        /// PublishUpdateUser. Sends a message to the message broker to update a user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task PublishUpdateUser(UserModel userModel);

        /// <summary>
        /// PublishUserLogInOut. Sends a message to the message broker to log in or out a user.
        /// </summary>
        /// <param name="userLogInOutModel"></param>
        /// <returns></returns>
        Task PublishUserLogInOut(UserLogInOutModel userLogInOutModel);
    }
}
