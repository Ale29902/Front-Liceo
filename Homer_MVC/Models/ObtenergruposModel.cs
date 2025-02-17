using Homer_MVC.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class ObtenergruposModel 
    {

        [Required(ErrorMessage = "Debe especificar al menos una materia.")]
        public List<Materia> materias { get; set; }

        // Día de la semana
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [RegularExpression("^(Lunes|Martes|Miércoles|Jueves|Viernes|Sábado|Domingo)$",
            ErrorMessage = "El día de la semana debe ser válido.")]
        public string DiaSemana { get; set; }

        // Nombre de la materia
        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        public string materia { get; set; }

        // ID del profesor
        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del profesor debe ser un valor válido.")]
        public int? id_Profesor { get; set; }

        // ID del grupo
        [Required(ErrorMessage = "El ID del grupo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del grupo debe ser un valor válido.")]
        public int? idgrupo { get; set; }



    }

}

