using ApiCQRS.Application.Services.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiCQRS.Application.Services.Events
{
    public class LogEventHandler :
                  INotificationHandler<UserActionNotification>,
                  INotificationHandler<ErrorNotification>
    {
        public Task Handle(UserActionNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"User {notification.Name} - {notification.Email} was { notification.Action.ToString().ToLower()} successfuly");
            });
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERROR : '{notification.Error} \n {notification.Stack}'");
            });
        }
    }
}
