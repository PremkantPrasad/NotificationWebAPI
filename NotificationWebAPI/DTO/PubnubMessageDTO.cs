using System.ComponentModel.DataAnnotations;

namespace PubnubMicroService.DTO
{
    public class NotificationDTO
    {
        [Required]
        public string Channel { get; set; }
        [Required]
        public object Message { get; set; }
    }
}