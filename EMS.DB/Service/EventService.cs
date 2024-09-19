using EMS.DB.Models;
using EMS.DB.Service.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Service
{
    public class EventService : IEventService
    {
        #region Property
        private IRepository<Event> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public EventService(IRepository<Event> repository, AppDbContext appDbContext)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        //public List<Event> GetList() => _repository.GetAll();
        public List<Event> GetList() {
            return _appDbContext.EventList.Include(x => x.Inquiry).ToList();
        }

        public void Insert(Event eventModel)
        {
            if (eventModel.Id is 0) _repository.Insert(eventModel);
            else _repository.Update(eventModel);
        }

        public void Delete(long id)
        {
            Event events = _appDbContext.EventList.FirstOrDefault(c => c.Id.Equals(id));
            _repository.Remove(events);
            _repository.SaveChanges();
        }
    }
}
