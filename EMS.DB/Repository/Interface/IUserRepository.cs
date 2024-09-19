using EMS.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Repository.Interface
{
    public interface IUserRepository
    {
       
        public List<User> GetAllUser();
        public List<User> GetById(string id);


        public void Insert(User userModel);
        public void Delete(string id);
        public void Update(User UserModel);


    }
}
