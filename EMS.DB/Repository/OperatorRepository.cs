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
    public class OperatorRepository : BaseRepository,IOperatorRepository
    {
        #region Property
        private IRepository<Operator> _repository;
        private AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public OperatorRepository(IRepository<Operator> repository, AppDbContext appDbContext, IServiceScopeFactory serviceScopeFactor) : base(serviceScopeFactor)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }
        #endregion
        public void Delete(long id)
        {
            //Operator Operators = _appDbContext.Operators.FirstOrDefault(c => c.Id.Equals(id));
            //_repository.Remove(Operators);

            using AppDbContext _myContext = base.GetContext();
            Operator Operator = _myContext.Operators.FirstOrDefault(c => c.Id.Equals(id));
            _myContext.Operators.Remove(Operator);
            _myContext.SaveChanges();
        }

        public List<Operator> GetById(long Id)
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Operators.Include(x => x.User).Where(e=>e.Id==Id).ToList();
        }

        //public List<Operator> GetOperatorList()
        //{
        //    return _appDbContext.Operators.ToList();


        //}
        public List<Operator> GetLists()
        {
            using AppDbContext _myContext = base.GetContext();
            return _myContext.Operators.Include(x => x.User).ToList();
            //_appDbContext.Events.Include(x => x.Category).ToList();
            //return _appDbContext.Events.Include(x => x.Inquiry).ToList();
        }

        public void Insert(Operator OperatorModel)
        {
            //if (OperatorModel.Id is 0)
            //    _repository.Insert(OperatorModel);

            using AppDbContext _myContext = base.GetContext();
            if (OperatorModel.Id is 0)
                _myContext.Operators.Add(OperatorModel);
            else
                _myContext.Operators.Update(OperatorModel);

            _myContext.SaveChanges();
        }
    }

}