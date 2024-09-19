using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository
{
    public class CategoryServiceRepository : BaseRepository, ICategoryServiceRepository
    {
        private IRepository<CategoryService> _repository;
        private AppDbContext _appDbContext;
        public CategoryServiceRepository(IRepository<CategoryService> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }

        public List<CategoryService> GetList()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.CategoryServices.ToList();
        }

        public List<CategoryService> GetByservice(string service)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.CategoryServices.Where(x => x.ServiceName == service).ToList();
        }

        public void Insert(CategoryService categoryService)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.CategoryServices.Add(categoryService);
            _myContext.SaveChanges();
        }

        public void Delete(long id)
        {
            //CategoryService services = _appDbContext.CategoryServices.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(services);
            //_repository.SaveChanges();

            using AppDbContext _myContext = base.GetContext();
            CategoryService services = _myContext.CategoryServices.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.CategoryServices.Remove(services);
            _myContext.SaveChanges();
        }

        public void Update(CategoryService categoryService)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.CategoryServices.Update(categoryService);
            _myContext.SaveChanges();
            //_repository.Update(categoryService);
        }
        public void InsertOrUpdate(CategoryService categoryService)
        {
            using AppDbContext _myContext = base.GetContext();
            if (categoryService.Id is 0)
                _myContext.CategoryServices.Add(categoryService);
            else
                _myContext.CategoryServices.Update(categoryService);

            _myContext.SaveChanges();
        }
    }
}
