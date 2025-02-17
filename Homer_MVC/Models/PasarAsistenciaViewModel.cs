﻿using Homer_MVC.Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homer_MVC.Models
{
    public class PasarAsistenciaViewModel
    {

      
        public DateTime? fecha { get; set; }

        // Lista de ausencias
        [Required(ErrorMessage = "Debe especificar al menos una ausencia.")]
        
        public List<Ausencia> ausencias { get; set; }

        // Validación personalizada para la fecha
     

    }
}