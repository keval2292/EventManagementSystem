using EMS.DB.Models;
using System.Collections.Generic;

namespace EMS.DB.Service.Interface
{
    public interface IEventService
    {
        public List<Event> GetList();
        public void Insert(Event eventModel);
        public void Delete(long id);
    }
}