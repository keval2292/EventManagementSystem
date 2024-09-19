using EMS.DB.Models;
using System.Collections.Generic;

namespace  EMS.DB.Repository.Interface
{
    public interface IEventCategoryRepository
    {
        public List<EventCategory> GetList();
        public void InsertOrUpdate(EventCategory categoryModel);
        public void Delete(long id);
        public EventCategory GetById(long id);
    }
}
