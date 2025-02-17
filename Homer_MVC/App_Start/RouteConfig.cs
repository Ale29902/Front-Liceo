using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homer_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignorar las rutas de archivos específicos como axd (archivos de recursos de ASP.NET)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ruta personalizada para el inicio de sesión
            routes.MapRoute(
           name: "AdminLogin",
           url: "admin/login",
           defaults: new { controller = "CommonViews", action = "Login" }
       );

            // Ruta personalizada para la validación de cuenta
            routes.MapRoute(
            name: "ProfesorLogin",
            url: "profesor/login",
            defaults: new { controller = "CommonViews", action = "Login2" }
        );

            // Ruta personalizada para cerrar sesión
            routes.MapRoute(
                name: "Logout",
                url: "cerrar-sesion", // Ruta personalizada
                defaults: new { controller = "CommonViews", action = "Logout" }
            );
            routes.MapRoute(
    name: "Logoutprof",
    url: "Account/Logoutprof",
    defaults: new { controller = "CommonViews", action = "Logoutprof" }
);
            routes.MapRoute(
    name: "Logoutadmin",
    url: "Account/Logoutadmin",
    defaults: new { controller = "CommonViews", action = "Logoutadmin" }
);
            routes.MapRoute(
           name: "MenuProfe2",  // Nombre de la ruta
           url: "MenuProfe2",   // URL de la ruta
           defaults: new { controller = "Dashboard", action = "MenuProfe2" }  // Controlador y acción para GET
       );
            routes.MapRoute(
                           name: "SeleccionFecha",
                           url: "Dashboard/SeleccionFecha",  // Esta es la URL que se usará
                           defaults: new { controller = "Dashboard", action = "SeleccionFecha" } // Nombre del controlador y acción
                       );
            
       

            // Ruta para PasarAsistencia (POST)
            routes.MapRoute(
                name: "PasarAsistencia",
                url: "Dashboard/PasarAsistencia",
                defaults: new { controller = "Dashboard", action = "PasarAsistencia" }
                
            );
            routes.MapRoute(
    name: "SeleccionGrupo",
    url: "Profe/Selecciongrupo", // Ruta específica
    defaults: new { controller = "Profe", action = "Selecciongrupo" }
);

            routes.MapRoute(
                name: "MenuProfePost",  // Nombre de la ruta para POST
                url: "MenuProfe",       // URL de la ruta
                defaults: new { controller = "Dashboard", action = "MenuProfe" }  // Controlador y acción para POST
            );
            routes.MapRoute(
     name: "RegistroProfesor",
     url: "registro-profesor", // Ruta personalizada para registro de profesores
     defaults: new { controller = "CommonViews", action = "Registro" }
 );
            routes.MapRoute(
    name: "Profesor",
    url: "Profesor/{action}/{id}",
    defaults: new { controller = "Profesor", action = "Index", id = UrlParameter.Optional }
);
            routes.MapRoute(
            name: "RegistroAlumno",
            url: "registro/registroalumno",  // Ruta personalizada
            defaults: new { controller = "CommonViews", action = "RegistroAlumno" }
        );
            routes.MapRoute(
           name: "Registromateria", // Nombre de la ruta
           url: "registro-materia", // URL personalizada
           defaults: new { controller = "CommonViews", action = "Registromateria" } // Mapea a la acción Registromateria del controlador CommonViews
       );

            routes.MapRoute(
                name: "Registrodemateria", // Nombre de la ruta
                url: "registrar-materia", // URL personalizada
                defaults: new { controller = "CommonViews", action = "Registrodemateria" } // Mapea a la acción Registrodemateria del controlador CommonViews
            );


            routes.MapRoute(
                name: "RegistrarAlumno",
                url: "registro/registraralumno",  // Ruta personalizada para POST
                defaults: new { controller = "CommonViews", action = "RegistrarAlumno" }
            );
            routes.MapRoute(
                name: "RegistrarProfesor",
                url: "registrar-profesor", // Ruta para registrar profesor
                defaults: new { controller = "CommonViews", action = "RegistrarProfesor" }
            );

            // Ruta predeterminada para manejar la URL básica sin especificar controlador o acción
            // Si no se especifica un controlador, se usará "InicioProfe" y si no se especifica una acción, se usará "Index"
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "InicioProfe", action = "Index", id = UrlParameter.Optional }
            );
          
         
        }
    }
}
