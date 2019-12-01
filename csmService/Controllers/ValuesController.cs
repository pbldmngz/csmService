using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace csmService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<alumno> Get()
        {
            using (var context = new csmEntities())
            {
                return context.alumnoes.ToList();
            }
        }

        // GET api/values/5
        // Obtener datos de alumno/profesor
        public Persona Get(int id, bool alumno)
        {
            using (var context = new csmEntities())
            {
                if (alumno)
                {
                    alumno a = context.alumnoes.Where(s => s.id_alumno == id).FirstOrDefault<alumno>();
                    Persona persona = new Persona(a.id_alumno, a.correo, a.id_grupo, a.nombre, a.primer_apellido, a.segundo_apellido);
                    return persona;
                }
                else
                {
                    profesor p = context.profesors.Where(e => e.id_empleado == id).FirstOrDefault<profesor>();
                    Persona persona = new Persona(p.id_empleado, p.correo, p.telefono, p.nombre, p.primer_apellido, p.segundo_apellido);
                    return persona;
                }
            }
        }

        //Obtener datos ordenados en lista
        public List<Persona> Get(bool alumno, int tamano_pagina, int numero_pagina)
        {
            List<Persona> ls = new List<Persona>();
            using (var context = new csmEntities())
            {
                if (alumno)
                {
                    var temp = context.Database.SqlQuery<alumno>("call paginado (@p1, @p2, @p3)",
                        new MySqlParameter("@p1", tamano_pagina),
                        new MySqlParameter("@p2", numero_pagina),
                        new MySqlParameter("@p3", alumno)).ToList();
                    foreach (alumno a in temp)
                    {
                        Persona persona = new Persona(a.id_alumno, a.correo, a.id_grupo, a.nombre, a.primer_apellido, a.segundo_apellido);
                        ls.Add(persona);
                    }
                }
                else
                {
                    var temp = context.Database.SqlQuery<profesor>("call paginado (@p1, @p2, @p3)",
                        new MySqlParameter("@p1", tamano_pagina),
                        new MySqlParameter("@p2", numero_pagina),
                        new MySqlParameter("@p3", alumno)).ToList();
                    foreach (profesor p in temp)
                    {
                        Persona persona = new Persona(p.id_empleado, p.correo, p.telefono, p.nombre, p.primer_apellido, p.segundo_apellido);
                        ls.Add(persona);
                    }
                }
            }
            return ls;
        }

        //Obtener los alumnos de un profesor
        public List<alumno> Get(int id)
        {
            using (var context = new csmEntities())
            {
                var temp = context.Database.SqlQuery<alumno>("call profesor_alumnos (@idx)", new MySqlParameter("@idx", id)).ToList();
                return temp;
            }
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
        }
    }
}
