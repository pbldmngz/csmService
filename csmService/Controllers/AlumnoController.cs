using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace csmService.Controllers
{
    public class AlumnoController : ApiController
    {
        [Route("alumno/listado")]
        [HttpGet]
        public IHttpActionResult Paginado(int pagina)
        {
            int tamano_pagina = 15;
            List<Persona> ls = new List<Persona>();
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<alumno>("call paginado (@p1, @p2, @p3)",
                    new MySqlParameter("@p1", tamano_pagina),
                    new MySqlParameter("@p2", pagina - 1),
                    new MySqlParameter("@p3", true)).ToList();
                foreach (alumno a in temp)
                {
                    Persona persona = new Persona(a.id_alumno, a.correo, a.id_grupo, a.nombre, a.primer_apellido, a.segundo_apellido);
                    ls.Add(persona);
                }
            }
            return Ok(ls);
        }

        [Route("alumno/ver")]
        [HttpGet]
        public IHttpActionResult Ver(int id)
        {
            using (var context = new csmEntities())
            {
                    alumno a = context.alumnoes.Where(s => s.id_alumno == id).FirstOrDefault<alumno>();
                    Persona persona = new Persona(a.id_alumno, a.correo, a.id_grupo, a.nombre, a.primer_apellido, a.segundo_apellido);
                    return Ok(persona);
            }
        }
    }
}