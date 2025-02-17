using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homer_MVC.Models.Entidades.Response
{
    public class ResInicioAdm : ResBase
    {
        public Administrador admin {  get; set; }
    }
}