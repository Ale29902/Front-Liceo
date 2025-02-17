using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class Materia 
    {

        public int Id_Profe { get; set; }
        public int Id_Materia { get; set; }
        public string usuarioprofe { get; set; }
        public string Nombre { get; set; }
        public int? Id_Grupo { get; set; }
        public int? Grado { get; set; }
        public int? Grupo1 { get; set; }
        public int? Id_Horario { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
