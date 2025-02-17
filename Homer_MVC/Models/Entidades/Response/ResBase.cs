using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homer_MVC.Models.Entidades
{
    public class ResBase
    {
        public bool respuesta {  get; set; }
        public List<Error>listaDeErrores {  get; set; }
    }
}