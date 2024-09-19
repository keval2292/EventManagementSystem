using EMS.DB.Models;
using System;
using System.Collections.Generic;

namespace  EMS.DB.Repository.Interface
{
    public interface IEventRepository
    {
        public List<Event> GetList();
        public Event GetById(long id);
        public List<Event> GetByOpertorId(string id);
        public List<Event> GetListToday(string id);
        public List<Event> CheckEvent(long id);
        public void Insert(Event eventModel);
        public void Update(Event eventModel);
        public void Delete(long id);
        public void SaveChanges();
        public List<Event> GetListFromDashboard(DateTime? startDate, DateTime? endDate);
    }
}