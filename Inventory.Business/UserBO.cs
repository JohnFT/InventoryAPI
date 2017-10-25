using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Business.Entities;

namespace Inventory.Business
{
    public class UserBO
    {
        private IRepository<User> _userRepo;

        public UserBO()
        {
            this._userRepo = new Repository<User>();
        }

        public Result<User> GetUserById(int id)
        {
            try
            {
                return new Result<User>() { Success = 200, Data = this._userRepo.Find(id) };
            }
            catch (Exception ex)
            {
                return new Result<User>() { Success = 500, Message = ex.Message };
            }
        }
    }
}
