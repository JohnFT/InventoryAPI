using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Business.Entities;

namespace Inventory.Business
{
    public class SessionBO
    {
        private IRepository<Session> _sessRepo;
        private IRepository<User> _userRepo;

        public SessionBO()
        {
            this._sessRepo = new Repository<Session>();
            this._userRepo = new Repository<User>();
        }

        public Result<Session> Login(string userName, string passwd)
        {
            try
            {
                var res = this._userRepo.Filter(u => u.Name == userName && u.Password == passwd).ToList();
                if (res.Count > 0)
                {
                    Session sess = new Session() {
                        Token = Guid.NewGuid().ToString().Replace("-", ""),
                        BeginDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMinutes(20),
                        UserId = res[0].Id
                    };
                    this._sessRepo.Add(sess);
                    sess.User = res[0];

                    return new Result<Session>() { Success = 200, Data = sess };
                }
                return new Result<Session>() { Success = 200, Message = "Usuario y/o contraseña incorrectos" };
            }
            catch (Exception ex)
            {
                return new Result<Session>() { Success = 500, Message = ex.Message };
            }
        }

        public Result<Session> validateSession(string toke)
        {
            try
            {
                var session = this._sessRepo.Filter(s => s.Token == toke).ToList();
                if(session.Count > 0){
                    if (session[0].EndDate < DateTime.Now)
                    {
                        return new Result<Session>() { Success = 401, Message = "Session expirada" };
                    }
                    return new Result<Session>() { Success = 200, Data = session[0] };
                }
                else
                {
                    return new Result<Session>() { Success = 400, Message = "Token invalido" };
                }
            }catch(Exception ex)
            {
                return new Result<Session>() { Success = 500, Message = ex.Message };
            }
        }
    }
}
