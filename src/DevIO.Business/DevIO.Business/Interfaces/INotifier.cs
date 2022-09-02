using DevIO.Business.Notifications;

namespace DevIO.Business.Interfaces;

public interface INotifier
{
    bool HaveNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);

}
