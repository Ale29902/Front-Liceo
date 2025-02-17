using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class ResSeleccionarAusencia: ResBase
    {
        public List<Asistencia> ausencias { get; set; }

    }
}
