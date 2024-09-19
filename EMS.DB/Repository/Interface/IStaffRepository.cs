using EMS.DB.Models;
using System.Collections.Generic;

namespace EMS.DB.Repository.Interface
{
    public interface IStaffRepository
    {
        public List<Staff> GetList();
        public List<Staff> GetStaffByserviceList(long service);
        public List<Staff> GetById(long id);
        public Staff GetStaff(long id);
        public List<Staff> GetStaffByUserId(string id);
        public void Insert(Staff eventModel);
        public void Update(Staff eventModel);
        public void Delete(long id);

    }
}