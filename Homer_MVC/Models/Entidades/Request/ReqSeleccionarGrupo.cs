using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades.Request
{
    public class ReqSeleccionarGrupo 
    {
        public int idGrupo { get; set; }
        public int idProfe { get; set; }
        public string dia { get; set; }

        public string materia { get; set; }

    }
}
