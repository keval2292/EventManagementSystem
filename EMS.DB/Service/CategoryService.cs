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
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _repository;
        private AppDbContext _appDbContext;

        #region Constructor
        public CategoryService(IRepository<Category> repository, AppDbContext appDbContext)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }

        #endregion
    
        public List<Category> GetList() => _repository.GetAll();

        public void Insert(Category categoryModel)
        {
            if (categoryModel.Id is 0) _repository.Insert(categoryModel);
            else _repository.Update(categoryModel);
        }
        public void Delete(long id)
        {
            Category categorys = _appDbContext.CategoryList.FirstOrDefault(c => c.Id.Equals(id));
            _repository.Remove(categorys);
            _repository.SaveChanges();
        }

       
    }
}
