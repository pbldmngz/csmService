using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace csmService.Controllers
{
    public class MensajeController : ApiController
    {
        [Route("mensaje/enviar")]
        [HttpPost]
        public IHttpActionResult Enviar([FromBody] object json)
        {
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            string id_emisor = (string)o["id_emisor"];
            string id_receptor = (string)o["id_receptor"];
            string mensaje = (string)o["mensaje"];

            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<bool>("call crear_mensaje (@id_emisor, @id_receptor, @mensaje)",
                    new MySqlParameter("@id_emisor", id_emisor),
                    new MySqlParameter("@id_receptor", id_receptor),
                    new MySqlParameter("@mensaje", mensaje)).ToList();

                context.SaveChanges();
                return Ok(temp);
            }
        }

        [Route("mensaje/ver")]
        [HttpPost]
        public IHttpActionResult Ver([FromBody] object json)
        {
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            int id = (int)o["id"];
            int pagina = (int)o["pagina"];
            int tamano = 15;

            List<Mensaje> ls = new List<Mensaje>();
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<Mensaje>("call buzon (@id, @tamano, @pagina)",
                    new MySqlParameter("@id", id),
                    new MySqlParameter("@tamano", tamano),
                    new MySqlParameter("@pagina", pagina - 1)).ToList();

                foreach (Mensaje m in temp)
                {
                    Mensaje mensaje = new Mensaje(m.id_emisor, m.contenido, m.fecha);
                    ls.Add(mensaje);
                }
                return Ok(ls);
            }
        }
    }
}