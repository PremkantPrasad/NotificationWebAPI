using PubnubMicroService.DTO;
using ServiceLayer;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;


namespace PubnubMicroService.Controllers
{
    public class NotificationController: ApiController
    {
        readonly INotificationHandler _notificationHandler;
        public NotificationController(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        [HttpPost]
        [Route("api/notification")]
        public HttpResponseMessage Post([FromBody] NotificationDTO notificationDTO)
        {
            try
            {
                _notificationHandler.GetLogger().Info("Post");
                if (notificationDTO != null && ModelState.IsValid)
                {
                    _notificationHandler.GetLogger().Info(string.Format("{0},{1},{2}", notificationDTO.Channel, notificationDTO.Message.ToString(), "Info"));

                    _notificationHandler.PublishMessage(notificationDTO.Channel, notificationDTO.Message);

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch(Exception ex)
            {
                _notificationHandler.GetLogger().Error(ex, string.Format("{0},{1},{2}", notificationDTO.Channel, notificationDTO.Message.ToString(), "Info"));
            }
            
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

    }
}