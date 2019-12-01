using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csmService.Controllers
{
    public class grupoAlumno
    {
        public grupoAlumno() { }
        public grupoAlumno(int _grupos, int _alumnos)
        {
            grupos = _grupos;
            alumnos = _alumnos;

        }
        public int grupos { get; set; }
        public int alumnos { get; set; }
    }
}