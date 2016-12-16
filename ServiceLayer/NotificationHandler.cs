using PubNubMessaging.Core;
using System;
using System.Configuration;
using NLog;

namespace ServiceLayer
{
    public class NotificationHandler : INotificationHandler
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        readonly Pubnub _pubnub = null;
        public NotificationHandler()
        {
            _logger.Error($"{"obj.Channel"},{"obj.Message"},{"obj.StatusCode"}");
            var publishKey  = ConfigurationManager.AppSettings["PublishKey"].ToString();
            var subscribeKey = ConfigurationManager.AppSettings["SubscribeKey"].ToString();
            var secretKey = ConfigurationManager.AppSettings["SecretKey"].ToString();
            if( _pubnub == null )
            {
                _pubnub = new Pubnub(publishKey, subscribeKey, secretKey, "", true);
            }
        }

        public Logger GetLogger()
        {
            return _logger;
        }
        public void PublishMessage(string channel, object message)
        {
            try
            {
                _pubnub.Publish<object>(channel, message, userCallback, errorCallback);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, string.Format("{0},{1},{2}", channel, message.ToString(), "Error"));
            }
        }

        private void userCallback(object obj)
        {
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            _logger.Info("Success" + message);
        }
        private void errorCallback(PubnubClientError obj)
        {
            _logger.Error($"{obj.Channel},{obj.Message},{obj.StatusCode}");
        }
    }
}
