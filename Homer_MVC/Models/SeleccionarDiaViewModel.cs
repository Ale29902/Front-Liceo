using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class SeleccionarDiaViewModel 
    {
        //
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [RegularExpression("^(lunes|martes|miércoles|jueves|viernes|sábado|domingo)$",
    ErrorMessage = "El día de la semana debe ser uno de los siguientes: lunes, martes, miércoles, jueves, viernes")]
        public string DiaSemana { get; set; }
    }
}
