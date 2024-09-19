using EMS.DB.Models;
using System.Collections.Generic;

namespace EMS.DB.Service.Interface
{
    public interface ICategoryService
    {
        public List<Category> GetList();
        public void Insert(Category categoryModel);
        public void Delete(long id);
    }
}
