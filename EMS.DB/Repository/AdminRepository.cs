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
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        #region Property
        private IRepository<Admin> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public AdminRepository(IRepository<Admin> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion
        
        public void Insert(Admin AdminModel)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.Admins.Add(AdminModel);
            _myContext.SaveChanges();
        }

        public void Update(Admin AdminModel)
        {
            using AppDbContext _myContext = base.GetContext();
            _myContext.Admins.Update(AdminModel);
            _myContext.SaveChanges();
        }

        public List<Admin> GetList()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Admins.ToList();
        }
    }
}
