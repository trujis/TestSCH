using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Users;
using Users.Get;
using Users.Models;
using WebApp.Handlers;

namespace WebApp.Controllers
{

    
    [UserAuthorization]
    public class UserController : ApiController
    {


        IUserService userService; //IOC


        [HttpGet]
        public IHttpActionResult Get()
        {
            var user = GetUser();

            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(user);
            }
        }


        [HttpGet]
        public IHttpActionResult List()
        {
            userService = new UserService(); //IOC

            var user = userService.ListNames();

            return Ok(user);
        }


        private User GetUser()
        {
            User response = null;

            if (User != null
                && User.Identity is BasicAuthenticationIdentity auth
                && auth.User != null)
            {
                response = auth.User;
            }

            return response;
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            else
            {
                userService = new UserService(); //IOC
                
                if (!userService.Exists(user.UserName))
                {
                    try
                    {
                        user.Save();
                        return Ok(true);
                    }
                    catch
                    {
                        return Ok(false);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Conflict);
                }
                
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            else
            {
                userService = new UserService(); //IOC

                userService.Delete(userName);
                return Ok(true);
            }
        }

        /*
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }*/
    }
}
