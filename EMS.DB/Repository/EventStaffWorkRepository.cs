using EMS.DB.Constant;
using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMS.DB.Repository
{
    public class EventStaffWorkRepository : BaseRepository, IEventStaffWorkRepository
    {
        #region Property
        private IRepository<EventStaffWork> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public EventStaffWorkRepository(IRepository<EventStaffWork> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }


        #endregion
        //public void Delete(long id)
        //{
        //    //OperatorWork OperatorWorks = _appDbContext.OperatorWorks.FirstOrDefault(c => c.Id.Equals(id));
        //    //_repository.Remove(OperatorWorks);
        //    //_repository.SaveChanges();

        //    using AppDbContext _myContext = base.GetContext();
        //    EventStaffWork EventStaffWorks = _myContext.EventStaffWorks.FirstOrDefault(c => c.Id.Equals(id));
        //    _myContext.EventStaffWorks.Remove(EventStaffWorks);
        //    _myContext.SaveChanges();
        //}
        public void Update(EventStaffWork EventStaffWorkModel)
        {
            //_repository.Update(OperatorWorkModel);
            using AppDbContext _myContext = base.GetContext();
            if (EventStaffWorkModel.Id > 0)
            {
                _myContext.EventStaffWorks.Update(EventStaffWorkModel);
                _myContext.SaveChanges();
            }
        }

        //public List<OperatorWork> GetOperatorWorkList() => _repository.GetAll();
        public List<EventStaffWork> GetEventStaffWorkList()
        {

            //_appDbContext.OperatorWorks.Include(x => x.Staff).ToList();
            //return _appDbContext.OperatorWorks.Include(x => x.Event).ToList();

            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(x => x.Event).ToList();

        }
        public void Insert(EventStaffWork EventStaffWorkModel)
        {
            //if (OperatorWorkModel.Id is 0)
            //    _repository.Insert(OperatorWorkModel);
            //else _repository.Update(OperatorWorkModel);

            using AppDbContext _myContext = base.GetContext();
            if (EventStaffWorkModel.Id is 0)
                _myContext.EventStaffWorks.Add(EventStaffWorkModel);
            else
                _myContext.EventStaffWorks.Update(EventStaffWorkModel);


            _myContext.SaveChanges();
        }
        public List<EventStaffWork> GetListFromWork(DateTime? startDate, DateTime? endDate)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(e =>e.Event).Where(c => c.Event.FromDate >= startDate.Value && c.Event.FromDate <= endDate.Value).ToList();
        }
        public List<EventStaffWork> GetListByStaff(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(e => e.Staff).Include(e=>e.Event).Where(c => c.Staff.UserId ==id ).ToList();
        }
        public List<EventStaffWork> GetListBystatus(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(e => e.Staff).Include(e => e.Event).Where(c => c.Staff.UserId == id && c.Status==Status.pending.ToString() || c.Status == Status.OnProcess.ToString()).ToList();
        }

        public List<EventStaffWork> GetListFromEvent(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Where(c => c.EventId == id).ToList();
        }

        public List<EventStaffWork> GetListToday()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(e => e.Event).Where(c => c.Event.FromDate == DateTime.Today).ToList();
        }

        public List<EventStaffWork> GetListBystatusFinish(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventStaffWorks.Include(e => e.Staff).Include(e => e.Event).Where(c => c.Staff.UserId == id && c.Status == Status.Finish.ToString() ).ToList();
        }
    }
}