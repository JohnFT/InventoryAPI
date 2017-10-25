using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Inventory.Business.Entities;
using Inventory.Business;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;

namespace Inventory.API
{
    public class InventoryAuthorizeAttribute : AuthorizeAttribute
    {
        private SessionBO sessionBO;

        public InventoryAuthorizeAttribute()
        {
                this.sessionBO = new SessionBO();
     
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.GetValues("Authorization") != null)
                {
                    string authenticationToken = Convert.ToString(actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault());

                    var result = this.sessionBO.validateSession(authenticationToken);
                    if (result.Success == 500)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
                        actionContext.Response.ReasonPhrase = result.Message;
                        return false;
                    }

                    if (result.Success == 401 || result.Success == 400)
                    {
                        HttpContext.Current.Response.AddHeader("Authorization", authenticationToken);
                        HttpContext.Current.Response.AddHeader("Authorization", "No Autorizado");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                        return false;
                    }

                    return true;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    actionContext.Response.ReasonPhrase = "No existe un token de autorización";
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Aqui se maneja el error
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
                actionContext.Response.ReasonPhrase = ex.Message;
                return false;
            }
        }

    }
}