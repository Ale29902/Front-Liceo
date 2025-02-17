using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class Ausencia
    {
        public int? id_Asistencia { get; set; }
        public int? id_Grupo { get; set; }
        public int? id_Alumno { get; set; }

        public string nombre_Alumno { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int? id_Profe { get; set; }
        public string dia { get; set; }
        public DateTime? fecha { get; set; }
        public string materia { get; set; }
        public string estado { get; set; }

    }
}
