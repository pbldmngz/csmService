using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace csmService.Controllers
{
    public class LoginController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] object json)
        {
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            string correo = (string)o["correo"];
            string password = (string)o["password"];

            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<bool>("call login (@correo, @password)",
                    new MySqlParameter("@correo", correo),
                    new MySqlParameter("@password", password)).ToList();

                return Ok(temp);
            }
        }

        [Route("login/password")]
        [HttpPut]
        public IHttpActionResult Password([FromBody] object json)
        {
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            string correo = (string)o["correo"];
            string old_pass = (string)o["old_pass"];
            string new_pass = (string)o["new_pass"];

            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<bool>("call cambiar_password (@correo, @old_pass, @new_pass)",
                    new MySqlParameter("@correo", correo),
                    new MySqlParameter("@old_pass", old_pass),
                    new MySqlParameter("@new_pass", old_pass)).ToList();

                context.SaveChanges();
                return Ok(temp);
            }
        }
    }
}