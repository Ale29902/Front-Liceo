using Homer_MVC.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class SeleccionFechaViewModel 
    {

        // Día de la semana
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [RegularExpression("^(lunes|martes|miércoles|jueves|viernes|sábado|domingo)$",
            ErrorMessage = "El día de la semana debe ser uno de los siguientes: lunes, martes, miércoles, jueves, viernes, sábado o domingo.")]
        public string DiaSemana { get; set; }

        // Materia
        [Required(ErrorMessage = "La materia es obligatoria.")]
        public string materia { get; set; }

        // ID del profesor (opcional)
        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        public int? id_Profesor { get; set; }

        // ID del grupo (opcional)
        [Required(ErrorMessage = "El ID del grupo es obligatorio.")]
        public int? idgrupo { get; set; }

        // ID del alumno (opcional)
        [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        public int? idalumno { get; set; }

        // Fecha de la clase
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime fecha { get; set; }

        // Estado (Presente, Ausente o Tardía)
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(Presente|Ausente|Tardía)$", ErrorMessage = "El estado debe ser 'Presente', 'Ausente' o 'Tardía'.")]
        public string estado { get; set; }

        // Día del evento
        [Required(ErrorMessage = "El día es obligatorio.")]
        [RegularExpression("^(lunes|martes|miércoles|jueves|viernes|sábado|domingo)$",
            ErrorMessage = "El día debe ser uno de los siguientes: lunes, martes, miércoles, jueves, viernes, sábado o domingo.")]
        public string dia { get; set; }

    }
}
