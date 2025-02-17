using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AlumnoViewModel
    {
        // Propiedad para el nombre del estudiante
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        // Propiedad para el primer apellido del estudiante
        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        public string Apellido1 { get; set; }

        // Propiedad para el segundo apellido del estudiante
        [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
        public string Apellido2 { get; set; }

        // Propiedad para el número de cédula del estudiante
        [Required(ErrorMessage = "El número de cédula es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "Número de cédula inválido.")]
        public int? NumeroCedula { get; set; }

        // Propiedad para el número de contacto del estudiante
        [Required(ErrorMessage = "El número de contacto es obligatorio.")]
        [Phone(ErrorMessage = "Número de contacto inválido.")]
        public string Contacto { get; set; }

        // Propiedad para el grupo al que pertenece el estudiante
        [Required(ErrorMessage = "El grupo es obligatorio.")]
        public int? Grupo { get; set; }

        // Propiedad para el grado del estudiante
        [Required(ErrorMessage = "El grado es obligatorio.")]
        public int? Grado { get; set; }
    }
}
