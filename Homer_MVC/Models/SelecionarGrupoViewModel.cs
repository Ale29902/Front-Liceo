using Homer_MVC.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class SelecionarGrupoViewModel 
    {
        public List<Asistencia>estudiantes{ get; set; }

        // Día de la semana
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [RegularExpression("^(Lunes|Martes|Miércoles|Jueves|Viernes)$",
            ErrorMessage = "El día de la semana debe ser uno de los siguientes: lunes, martes, miércoles, jueves, viernes.")]
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

        // Nombre del alumno
        [Required(ErrorMessage = "El nombre del alumno es obligatorio.")]
        public string nombre_Alumno { get; set; }

        // Primer apellido del alumno
        [Required(ErrorMessage = "El primer apellido del alumno es obligatorio.")]
        public string apellido1 { get; set; }

        // Segundo apellido del alumno
        public string apellido2 { get; set; }  // Apellido 2 es opcional

        // ID del alumno (opcional)
        [Required(ErrorMessage = "El ID del alumno es obligatorio.")]
        public int? idalumno { get; set; }

        // Fecha de la clase
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? Fecha { get; set; }

        // Estado (Presente, Tardía, Ausente)
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(Presente|Tardío|Ausente)$", ErrorMessage = "El estado debe ser 'Presente', 'Tardío' o 'Ausente'.")]
        public string estado { get; set; }

        // Día del evento
        [Required(ErrorMessage = "El día es obligatorio.")]
        [RegularExpression("^(Lunes|Martes|Miércoles|Jueves|Viernes)$",
            ErrorMessage = "El día debe ser uno de los siguientes: lunes, martes, miércoles, jueves, viernes.")]
        public string dia { get; set; }


    }
}
