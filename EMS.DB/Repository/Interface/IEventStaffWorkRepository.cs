using EMS.DB.Models;
using System;
using System.Collections.Generic;

namespace EMS.DB.Repository.Interface
{
    public interface IEventStaffWorkRepository
    {
        public List<EventStaffWork> GetEventStaffWorkList();
        public List<EventStaffWork> GetListFromWork(DateTime? startDate, DateTime? endDate);
        public List<EventStaffWork> GetListToday();
        public List<EventStaffWork> GetListFromEvent(long id);
        public List<EventStaffWork> GetListBystatus(string id);
        public List<EventStaffWork> GetListBystatusFinish(string id);
        public List<EventStaffWork> GetListByStaff(string id);
        public void Insert(EventStaffWork EventStaffWorkModel);
        public void Update(EventStaffWork EventStaffWorkModel);
        
    }
}
