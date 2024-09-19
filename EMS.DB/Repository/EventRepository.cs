using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        #region Property
        private IRepository<Event> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public EventRepository(IRepository<Event> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        //public List<Event> GetList() => _repository.GetAll();
        public List<Event> GetList()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.Include(x => x.Inquiry).Include(x => x.Operator.User).Include(x => x.Category).ToList();
            //_appDbContext.Events.Include(x => x.Category).ToList();
            //return _appDbContext.Events.Include(x => x.Inquiry).ToList();
        }

        public Event GetById(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.FirstOrDefault(x => x.Id == id);
            //return _repository.GetById(id);
        }
        public List<Event> GetByOpertorId(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.Include(x => x.Operator.User).Include(x=>x.Category).Where(e=>e.Operator.UserId==id).ToList();
        }
        public List<Event> CheckEvent(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.Include(x => x.Inquiry).Where(e => e.InquiryId== id).ToList();
        }
        public List<Event> GetListToday(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.Where(c => c.Operator.UserId == id&& c.FromDate == DateTime.Today).ToList();
        }
        public void Insert(Event eventModel)
        {
            using AppDbContext _myContext = base.GetContext();
            if (eventModel.Id is 0)
                _myContext.Events.Add(eventModel);
            else
                _myContext.Events.Update(eventModel);
            
            
            
            _myContext.SaveChanges();


            //if (eventModel.Id is 0) 
            //    _repository.Insert(eventModel);
            //else _repository.Update(eventModel);


        }

        public void Update(Event eventModel)
        {
            using AppDbContext _myContext = base.GetContext();
            if (eventModel.Id is 0)
                _myContext.Events.Add(eventModel);
            else
                _myContext.Events.Update(eventModel);

            _myContext.SaveChanges();
        }

        public void SaveChanges()
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.SaveChanges();
        }

        public void Delete(long id)
        {
            //Event events = _appDbContext.Events.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(events);
            //_repository.SaveChanges();

            using AppDbContext _myContext = base.GetContext();
            Event events = _myContext.Events.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.Events.Remove(events);
            _myContext.SaveChanges();
        }

        public List<Event> GetListFromDashboard(DateTime? startDate, DateTime? endDate)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Events.Include(x => x.Operator.User).Where(c => c.FromDate >= startDate.Value && c.FromDate <= endDate.Value).ToList();
        }
    }
}
