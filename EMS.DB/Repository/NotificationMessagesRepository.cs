using EMS.DB.Constant;
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
    public class NotificationMessagesRepository : BaseRepository,INotificationMessagesRepository
    {
        #region Property
        private IRepository<NotificationMessages> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public NotificationMessagesRepository(IRepository<NotificationMessages> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
    {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion
        public async Task<List<NotificationMessages>> GetMessages(string userId)
        {
            using AppDbContext _myContext = base.GetContext();
            return await _myContext.NotificationMessages.Include(x => x.User).Where(x => x.UserId == userId && x.MarkAsRead).OrderByDescending(x => x.CreatedOn).Take(50).ToListAsync();
        }


        public async Task InsertAsync(NotificationMessages NotificationMessageModel)
        {
            //if (NotificationMessageModel.Id is 0)
            //{
            //    using AppDbContext _myContext = base.GetContext();
            //    _myContext.Add(NotificationMessageModel);
            //    _myContext.SaveChanges();
            //}
            //else { _repository.Update(NotificationMessageModel)};
           
            string currentUserId = NotificationMessageModel.UserId;
            List<User> notifyUsers = new List<User>();
            await using AppDbContext _myContext = base.GetContext();
            //List of all user of current event
            var eventUsers = await _appDbContext.EventStaffWorks.Where(x => x.EventId == NotificationMessageModel.EventId).ToListAsync();
            var userList = await _appDbContext.Users.Where(x => x.IsActive).ToListAsync();
            //Current event Staff, Other roles
            foreach (var userRec in userList.Where(x => x.Userrole.Equals(Userrole.Staff.ToString(), StringComparison.OrdinalIgnoreCase) 
            || x.Userrole.Equals(Userrole.Operator.ToString(), StringComparison.OrdinalIgnoreCase)
            || x.Userrole.Equals(Userrole.Admin.ToString(), StringComparison.OrdinalIgnoreCase)
            ))
            {
                if (userRec.Userrole.Equals(Userrole.Staff.ToString(), StringComparison.OrdinalIgnoreCase)
                    && eventUsers is not null && eventUsers.Any(x => x.Staff is not null && x.Staff.UserId.Equals(userRec.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    notifyUsers.Add(userRec);
                }
                else {
                    notifyUsers.Add(userRec);
                }
            }

            if (notifyUsers.Any())
            {
                foreach (var userInfo in notifyUsers.Where(x => x.Id.Equals(currentUserId, StringComparison.OrdinalIgnoreCase) == false))
                {
                    NotificationMessageModel.UserId = userInfo.Id;
                    NotificationMessageModel.Id = Guid.NewGuid();
                    _myContext.NotificationMessages.Add(NotificationMessageModel);
                    _myContext.SaveChanges();
                }

            }
        }

        
    }
}
