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
    public class EventCategoryRepository : BaseRepository, IEventCategoryRepository
    {
        private IRepository<EventCategory> _repository;
        private AppDbContext _appDbContext;

        #region Constructor
        public EventCategoryRepository(IRepository<EventCategory> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }

        #endregion

        public List<EventCategory> GetList()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventCategories.ToList();
        }

        public EventCategory GetById(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.EventCategories.FirstOrDefault(x => x.Id == id);
        }

        public void InsertOrUpdate(EventCategory categoryModel)
        {
            using AppDbContext _myContext = base.GetContext();
            if (categoryModel.Id is 0)
                _myContext.EventCategories.Add(categoryModel);
            else
                _myContext.EventCategories.Update(categoryModel);

            _myContext.SaveChanges();
        }

        public void Delete(long id)
        {
            //EventCategory categorys = _appDbContext.EventCategories.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(categorys);
            //_repository.SaveChanges();

            using AppDbContext _myContext = base.GetContext();
            EventCategory categorys = _myContext.EventCategories.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.EventCategories.Remove(categorys);
            _myContext.SaveChanges();
        }

       
    }
}
