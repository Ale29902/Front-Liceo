using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class ProfesorViewModel 

    {
  
    // Nombre del usuario
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string nombre { get; set; }

    // Primer apellido del usuario
    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    public string apellido1 { get; set; }

    // Segundo apellido del usuario
    [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
    public string apellido2 { get; set; }

    // Correo del usuario
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string correo { get; set; }

    // Nombre de usuario
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    public string usuario { get; set; }

    // Contraseña
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string contra { get; set; }

    // Estado del usuario
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression("(Activo|Inactivo)", ErrorMessage = "El estado debe ser 'Activo' o 'Inactivo'.")]
    public string estado { get; set; }


    }
}
