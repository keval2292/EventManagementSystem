using EMS.DB.Models;
using System.Collections.Generic;

namespace EMS.DB.Repository.Interface
{
    public interface IAdminRepository
    {
        public List<Admin> GetList();
        public void Insert(Admin AdminModel);
        public void Update(Admin AdminModel);
    }
}