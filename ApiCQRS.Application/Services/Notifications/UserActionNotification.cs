using MediatR;

namespace ApiCQRS.Application.Services.Notifications
{
    public class UserActionNotification : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ActionNotification Action { get; set; }
    }

    public enum ActionNotification
    {
        Created = 1,
        Updated = 2,
        Deleted = 3
    }
}
