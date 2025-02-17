using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homer_MVC.Models.Entidades
{
    public class Administrador
    {
        public  int? idAdmin { get; set; }
        public string usuario { get; set; }
        public string contra { get; set; }
        public int? estado { get; set; }
     

    }
}