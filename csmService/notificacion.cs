//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace csmService
{
    using System;
    using System.Collections.Generic;
    
    public partial class notificacion
    {
        public int id_notificacion { get; set; }
        public string contenido { get; set; }
        public int id_emisor { get; set; }
        public int id_receptor { get; set; }
        public System.DateTime fecha { get; set; }
    
        public virtual profesor profesor { get; set; }
        public virtual profesor profesor1 { get; set; }
    }
}
