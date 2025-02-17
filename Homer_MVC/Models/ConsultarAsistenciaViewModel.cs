using Homer_MVC.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class ConsultarAsistenciaViewModel 
    {
        public DateTime? fechaConsulta { get; set; }

        // Lista de ausencias
        [Required(ErrorMessage = "Debe especificar al menos una ausencia.")]

        public List<Ausencia> ausencias { get; set; }
    }
}
