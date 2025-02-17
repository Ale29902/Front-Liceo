using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class ReqSeleccionarAusencia 
    {
        
        public List<Asistencia> asistencias { get; set; }
        public int? idprofe { get; set; }
        public string materia { get; set; }
        public DateTime fecha { get; set; }
        public int? idgrupo { get; set; }


    }
}
