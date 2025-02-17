using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homer_MVC.Models.Entidades
{
    public static class SesionProf
    {
        public static int? id_sesionprof { get; set; }
        public static string APELLIDO1 { get; set; }
        public static string CORREO_ELECTRONICO { get; set; }
        public static string APELLIDO2 { get; set; }
        
        public static string NOMBRE { get; set; }
        public static string usuario { get; set; }
        public static string estado { get; set; }

        public static DateTime ultimaAccion { get; set; }
        public static DateTime fechaInicio {  get; set; }

       

        private static readonly TimeSpan TiempoSesionMaximo = TimeSpan.FromMinutes(30);
        public static bool ComprobarSesion()
        {
            // Verificar si la sesión tiene una Id válida y no ha pasado el tiempo máximo de inactividad
            if (HttpContext.Current.Session["usuario"] != null &&
                DateTime.Now - (DateTime)HttpContext.Current.Session["ultimaAccion"] < TiempoSesionMaximo)
            {
                return true;
            }
            return false;
        }

        public static void CerrarSesionprof()
        {
            // Limpiar todos los valores de la sesión
            HttpContext.Current.Session["id_sesionprof"] = null;
            HttpContext.Current.Session["usuario"] = null;
            HttpContext.Current.Session["NOMBRE"] = null;
            HttpContext.Current.Session["APELLIDO1"] = null;
            HttpContext.Current.Session["APELLIDO2"] = null;
            HttpContext.Current.Session["ultimaAccion"] = null;
            HttpContext.Current.Session["fechaInicio"] = null;
        }

        public static void iniciarSesionprof(int id_sesionprofeq, string Usuario, string NOMBRE, string apellido1, string apellido2)
        {
            // Guardar la información de la sesión en Session
            HttpContext.Current.Session["id_sesionprof"] = id_sesionprofeq;
            HttpContext.Current.Session["usuario"] = Usuario;
            HttpContext.Current.Session["NOMBRE"] = NOMBRE;
            HttpContext.Current.Session["APELLIDO1"] = apellido1;
            HttpContext.Current.Session["APELLIDO2"] = apellido2;
            HttpContext.Current.Session["fechaInicio"] = DateTime.Now;
            HttpContext.Current.Session["ultimaAccion"] = DateTime.Now;
        }

    }
    public static class Sesionadmin
    {
        public static int? idAdmin { get; set; }
        public static string usuario { get; set; }
        public static int? estado { get; set; }

        public static DateTime ultimaAccion { get; set; }
        public static DateTime fechaInicio { get; set; }

       

        private static readonly TimeSpan TiempoSesionMaximo = TimeSpan.FromMinutes(30);
        public static bool ComprobarSesionadmin()
        {
            if (HttpContext.Current.Session["usuario"] != null &&
                DateTime.Now - (DateTime)HttpContext.Current.Session["fechaInicio"] < TimeSpan.FromMinutes(30))
            {
                return true;
            }
            return false;
        }

        public static void CerrarSesionadmin()
        {
            // Limpiar la sesión
            HttpContext.Current.Session["idAdmin"] = null;
            HttpContext.Current.Session["usuario"] = null;
            HttpContext.Current.Session["estado"] = null;
            HttpContext.Current.Session["fechaInicio"] = null;
        }

        public static void iniciarSesionadmin(int id_admin, string usuario, int estado)
        {
            HttpContext.Current.Session["idAdmin"] = id_admin;
            HttpContext.Current.Session["usuario"] = usuario;
            HttpContext.Current.Session["estado"] = estado;
            HttpContext.Current.Session["fechaInicio"] = DateTime.Now;
        }

    }
    public static class SeleccionGrupo
    {
        public static int? idGrupo { get; set; }
        public static string materia { get; set; }
        public static string DiaSemana { get; set; }


        public static void Volversesiongrupo()
        {
            // Limpiar la sesión
            HttpContext.Current.Session["idGrupo"] = null;
            HttpContext.Current.Session["materia"] = null;
            HttpContext.Current.Session["dia"] = null;

        }

        public static void iniciarSesiongrupo(int? id_grupo, string materia, string dia)
        {
            HttpContext.Current.Session["idGrupo"] = id_grupo;
            HttpContext.Current.Session["materia"] = materia;
            HttpContext.Current.Session["dia"] = dia;
        }
    }
    public static class SeleccionFechas
    {
        public static DateTime fecha { get; set; }
       


        public static void Volversesionfechas()
        {
            // Limpiar la sesión
            HttpContext.Current.Session["fecha"] = null;
          
        }

        public static void iniciarSesionfechas(DateTime fecha)
        {
            HttpContext.Current.Session["fecha"] = fecha;
            
        }
    }
    public static class Seleccionasistencia
    {
        public static List<Asistencia> asistencia { get; set; }


        public static void Volversesionasistencia()
        {
            // Limpiar la sesión
            HttpContext.Current.Session["asistencia"] = null;

        }

    }  }