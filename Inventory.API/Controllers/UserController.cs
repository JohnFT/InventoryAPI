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
    [InventoryAuthorize]
    public class UserController : ApiController
    {
        private UserBO _user;

        public UserController()
        {
            this._user = new UserBO();
        }

        [Route("api/User/GetUser")]
        [HttpGet]
        public Result<User> GetUser(int id)
        {
            return this._user.GetUserById(id);
        }
    }
}
