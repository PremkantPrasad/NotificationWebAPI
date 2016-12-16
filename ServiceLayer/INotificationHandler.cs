
using NLog;

namespace ServiceLayer
{
    public interface INotificationHandler
    {
        void PublishMessage(string channel, object message);
        Logger GetLogger();
    }
}
