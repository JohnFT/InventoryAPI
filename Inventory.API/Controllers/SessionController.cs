using Inventory.Business;
using Inventory.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.API.Controllers
{
    public class SessionController : ApiController
    {
        private SessionBO _sess;

        public SessionController()
        {
            this._sess = new SessionBO();
        }

        [HttpGet]
        [Route("api/Session/Login")]
        public Result<Session> Login(string userName, string passwd)
        {
            return this._sess.Login(userName, passwd);
        }

    }
}
