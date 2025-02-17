using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Homer_MVC.Models.CommonViewsModel;

using System.Web.UI.WebControls;
using Homer_MVC.Models.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Homer_MVC.Models
{
    public class MateriaViewModel
    {
        // Propiedades que corresponden a los campos del formulario

        // Usuario del profesor
        [Required(ErrorMessage = "El usuario del profesor es obligatorio.")]
        public string Usuarioprofe { get; set; }

        // Nombre de la materia
        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        public string Nombre { get; set; }

        // Grado
        [Required(ErrorMessage = "El grado es obligatorio.")]
        [Range(1, 12, ErrorMessage = "El grado debe estar entre 1 y 12.")]
        public int? Grado { get; set; }

        // Grupo
        [Required(ErrorMessage = "El grupo es obligatorio.")]
        [Range(1, 99, ErrorMessage = "El grupo debe ser un valor válido.")]
        public int? Grupo1 { get; set; }

        // Día de la semana
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        [RegularExpression("^(Lunes|Martes|Miércoles|Jueves|Viernes|Sábado|Domingo)$", ErrorMessage = "El día de la semana debe ser válido.")]
        public string DiaSemana { get; set; }

        // Hora de inicio
        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time, ErrorMessage = "La hora de inicio debe ser válida.")]
        public TimeSpan HoraInicio { get; set; }

        // Hora de fin
        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time, ErrorMessage = "La hora de fin debe ser válida.")]
        [CustomValidation(typeof(MateriaViewModel), nameof(ValidateHoras))]
        public TimeSpan HoraFin { get; set; }

        // Método de validación personalizada para verificar que HoraInicio < HoraFin
        public static ValidationResult ValidateHoras(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance as MateriaViewModel;

            if (instance == null)
            {
                return new ValidationResult("Error en la validación de las horas.");
            }

            if (instance.HoraInicio >= instance.HoraFin)
            {
                return new ValidationResult("La hora de inicio debe ser menor que la hora de fin.");
            }

            return ValidationResult.Success;
        }


    }
} 
