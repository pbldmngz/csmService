using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csmService.Controllers
{
    public class Persona
    {
        public Persona()
        {
        }
        public Persona(int _id, string _correo, string _tel_grupo, string _nombre, string _primer_apellido, string _segundo_apellido)
        {
            id = _id;
            correo = _correo;
            tel_grupo = _tel_grupo;
            nombre = _nombre;
            primer_apellido = _primer_apellido;
            segundo_apellido = _segundo_apellido;
        }
        public int id { get; set; }
        public string correo { get; set; }
        public string tel_grupo { get; set; }
        public string nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
    }
}