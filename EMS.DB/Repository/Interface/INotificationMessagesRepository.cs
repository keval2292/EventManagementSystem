using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository.Interface
{
    public interface INotificationMessagesRepository
    {
        Task<List<NotificationMessages>> GetMessages(string userId);
        Task InsertAsync(NotificationMessages NotificationMessageModel);
    }
}
