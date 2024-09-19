using EMS.DB.Models;
using EMS.DB.Repository.Interface;
using EMS.DB.unitofwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMS.DB.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
       
        #region property
        private readonly AppDbContext _applicationDbContext;
        private DbSet<User> entities;
        #endregion

        #region Constructor
        public UserRepository(AppDbContext applicationDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<User>();
        }
        #endregion

        public List<User> GetAllUser()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Users.ToList();
        }

        public void Insert(User userModel)
        {
            if (!string.IsNullOrEmpty(userModel.Id))
            {
                using AppDbContext _myContext = base.GetContext();
                _myContext.Users.Add(userModel);
                _myContext.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using AppDbContext _myContext = base.GetContext();
                User userModel = _myContext.Users.FirstOrDefault(c => c.Id.Equals(id));
                if (userModel is not null)
                { 
                    _myContext.Users.Update(userModel);
                    _myContext.SaveChanges();
                }
            }
        }

        public List<User> GetById(string id)
        {
            //throw new NotImplementedException();
            try
            {
                using AppDbContext _myContext = base.GetContext();
                return _myContext.Users.Where(x => x.Id == id).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(User UserModel)
        {
            using AppDbContext _myContext = base.GetContext();
            if (UserModel.Id is null)
                _myContext.Users.Add(UserModel);
            else
                _myContext.Users.Update(UserModel);

            _myContext.SaveChanges();
        }
    }
}
