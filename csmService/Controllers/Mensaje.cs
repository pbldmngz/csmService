using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csmService.Controllers
{
    public class Mensaje
    {
        public Mensaje()
        {
        }
        public Mensaje(int _id_emisor, string _contenido, string _fecha)
        {
            id_emisor = _id_emisor;
            contenido = _contenido;
            fecha = _fecha;
        }
        public int id_emisor { get; set; }
        public string contenido { get; set; }
        public string fecha { get; set; }
    }
}