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
    public class StaffRepository : BaseRepository, IStaffRepository
    {
        #region Property
        private IRepository<Staff> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public StaffRepository(IRepository<Staff> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion

        //public List<Staff> GetList() => _repository.GetAll();
        public List<Staff> GetList()
        {
            //return _appDbContext.Staffs.Include(x => x.User).ToList();
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Staffs.Include(x => x.User).ToList();

        }

        public List<Staff> GetStaffByserviceList(long service)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Staffs.Where(x => x.StaffService == service).ToList();
        }
        public List<Staff> GetStaffByUserId(string id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Staffs.Where(x => x.UserId == id).ToList();
        }
        public List<Staff> GetById(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.Staffs.Include(x => x.User).ToList();
            return _myContext.Staffs.Where(x => x.Id == id).ToList();

        }
        public Staff GetStaff(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.Staffs.Include(x => x.User).ToList();
            return _myContext.Staffs.Where(x => x.Id == id).FirstOrDefault();

        }

        public void Insert(Staff StaffModel)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.Staffs.Add(StaffModel);
            _myContext.SaveChanges();
        }
        public void Update(Staff StaffModel)
        {
            //_repository.Update(StaffModel);

            using AppDbContext _myContext = base.GetContext();
            _myContext.Staffs.Update(StaffModel);
            _myContext.SaveChanges();
        }
        public void Delete(long id)
        {
            using AppDbContext _myContext = base.GetContext();
            Staff staffs = _myContext.Staffs.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.Staffs.Remove(staffs);
            _myContext.SaveChanges();
        }
    }
}
