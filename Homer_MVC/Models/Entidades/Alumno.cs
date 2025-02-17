using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class Alumno 
    {
        public int id_Alumno { set; get; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int? numero_Cedula { set; get; }
        public string contacto { get; set; }
        public int? Grupo { get; set; }
        public int? Grado { get; set; }
    }
}
