using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace csmService.Controllers
{
    public class ProfesorController : ApiController
    {
        // GET: Profesor
        [Route("profesor/listado")]
        [HttpGet]
        public IHttpActionResult Listado(int pagina)
        {
            int tamano_pagina = 15;
            List<Persona> ls = new List<Persona>();
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<profesor>("call paginado (@p1, @p2, @p3)",
                        new MySqlParameter("@p1", tamano_pagina),
                        new MySqlParameter("@p2", pagina - 1),
                        new MySqlParameter("@p3", false)).ToList();
                foreach (profesor p in temp)
                {
                    Persona persona = new Persona(p.id_empleado, p.correo, p.telefono, p.nombre, p.primer_apellido, p.segundo_apellido);
                    ls.Add(persona);
                }
            }
            return Ok(ls);
        }

        [Route("profesor/alumnos")]
        [HttpPost]
        public IHttpActionResult Alumnos([FromBody] object json)
        {
            List<Persona> ls = new List<Persona>();
            JObject o = JObject.Parse(JsonConvert.SerializeObject(json));
            int id = (int)o["id"];
            int pagina = (int)o["pagina"];

            int tamano_pagina = 15;
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<alumno>("call profesor_alumnos (@id, @pagina, @t_pagina)", 
                    new MySqlParameter("@id", id), 
                    new MySqlParameter("@pagina", pagina - 1),
                    new MySqlParameter("@t_pagina", tamano_pagina)).ToList();
                foreach (alumno a in temp)
                {
                    Persona persona = new Persona(a.id_alumno, a.correo, a.id_grupo, a.nombre, a.primer_apellido, a.segundo_apellido);
                    ls.Add(persona);
                }
                return Ok(ls);
            }
        }

        [Route("profesor/ver")]
        [HttpGet]
        public IHttpActionResult Ver(int id)
        {
            using (var context = new csmEntities())
            {
                profesor p = context.profesors.Where(s => s.id_empleado == id).FirstOrDefault<profesor>();
                Persona persona = new Persona(p.id_empleado, p.correo, p.telefono, p.nombre, p.primer_apellido, p.segundo_apellido);
                return Ok(persona);
            }
        }

        [Route("profesor/grupo_alumno")]
        [HttpGet]
        public IHttpActionResult grupo_alumno(int id)
        {
            List<grupoAlumno> ls = new List<grupoAlumno>();
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<grupoAlumno>("call group_students (@p1)",
                    new MySqlParameter("@p1", id)).ToList(); ;
                foreach (grupoAlumno a in temp)
                {
                    grupoAlumno grupoAlumno = new grupoAlumno(a.alumnos, a.grupos);
                    ls.Add(grupoAlumno);
                }
                
                return Ok(ls);
            }
        }

        [Route("profesor/correo")]
        [HttpGet]
        public IHttpActionResult grupo_alumno(string correo)
        {
            using (var context = new csmEntities())
            {
                profesor p = context.profesors.Where(s => s.correo == correo).FirstOrDefault<profesor>();
                return Ok(p.id_empleado);
            }
        }
    }
}