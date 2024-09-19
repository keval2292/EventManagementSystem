using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DB.Models
{
    public class NotificationMessages : BaseEntity
    {
        public new Guid Id { get; set; }
        public string Message { get; set; } = "";
        public string Title { get; set; } = "";
        public string UserId { get; set; }
        public long EventId { get; set; }
        public bool MarkAsRead { get; set; } = true;
        public User User { get; set; }
    }
}
