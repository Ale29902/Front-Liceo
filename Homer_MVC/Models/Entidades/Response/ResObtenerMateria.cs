using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models.Entidades
{
    public class ResObtenerGrupo: ResBase
    {
      public List<Materia> grupos {  get; set; }
       
    }
}
