using Homer_MVC.Models;
using Homer_MVC.Models.Entidades;
using Homer_MVC.Models.Entidades.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Homer_MVC.Controllers
{
    public class CommonViewsController : Controller
    {

        [System.Web.Mvc.HttpGet] // Usa System.Web.Mvc.HttpGetAttribute, no System.Web.Http.HttpGetAttribute
        public ActionResult Login2()
        {
            if (SesionProf.ComprobarSesion())
            {
                return RedirectToAction("menuprofe", "Dashboard");
            }
            else
            {
                return View(new CommonViewsModel.Login2Model());  // ✅
            }
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login2(CommonViewsModel.Login2Model model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new ReqLoginProf
                    {
                        Profesor = new Profesores
                        {
                            usuario = model.Usuario,
                            contra = model.Password
                        }
                    };

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("https://localhost:44345/api/Profe/login", jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var res = JsonConvert.DeserializeObject<ResLoginProf>(responseContent);

                            if (res.respuesta)
                            {


                                SesionProf.id_sesionprof = res.Profesores.id_Profesor;

                                SesionProf.usuario = res.Profesores.usuario;
                          
                                SesionProf.NOMBRE= res.Profesores.nombre;
                                SesionProf.APELLIDO1 = res.Profesores.apellido1;
                                SesionProf.APELLIDO2 = res.Profesores.apellido2;

                                SesionProf.fechaInicio = DateTime.Now;
                                SesionProf.ultimaAccion = DateTime.Now;

                                return RedirectToAction("menuprofe", "Dashboard");
                            }
                            else
                            {
                                // Log de depuración
                                System.Diagnostics.Debug.WriteLine("Error en el inicio de sesión: " + string.Join(", ", res.listaDeErrores.Select(e => e.error)));

                                if (res.listaDeErrores != null)
                                {
                                    foreach (var error in res.listaDeErrores)
                                    {
                                        ModelState.AddModelError("", error.error);
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Error desconocido. Por favor, intente más tarde.");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error de comunicación con el servidor. Por favor, intente más tarde.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ha ocurrido un error inesperado: {ex.Message}");
                    // Log de depuración
                    System.Diagnostics.Debug.WriteLine("Excepción: " + ex.Message);
                }
            }

            return View(model);
        }

        [System.Web.Mvc.HttpGet] // Usa System.Web.Mvc.HttpGetAttribute, no System.Web.Http.HttpGetAttribute
        public ActionResult Login()
        {
            if (Sesionadmin.ComprobarSesionadmin())
            {
                return RedirectToAction("index", "Dashboard");
            }
            else
            {
                return View(new CommonViewsModel.LoginModel());  // ✅
            }
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(CommonViewsModel.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var req = new ReqInicioAdm
                    {
                        administrador = new Administrador
                        {
                            usuario = model.Usuario,
                            contra = model.Contraseña
                        }
                    };

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("https://localhost:44345/api/admin/login", jsonContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var res = JsonConvert.DeserializeObject<ResInicioAdm>(responseContent);

                            if (res.respuesta)
                            {
                               
                                Sesionadmin.usuario = res.admin.usuario;
                                Sesionadmin.idAdmin= res.admin.idAdmin;
                                Sesionadmin.estado=res.admin.estado;
                                Sesionadmin.fechaInicio = DateTime.Now;

                                return RedirectToAction("index", "Dashboard");
                            }
                            else
                            {
                                // Log de depuración
                                System.Diagnostics.Debug.WriteLine("Error en el inicio de sesión: " + string.Join(", ", res.listaDeErrores.Select(e => e.error)));

                                if (res.listaDeErrores != null)
                                {
                                    foreach (var error in res.listaDeErrores)
                                    {
                                        ModelState.AddModelError("", error.error);
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Error desconocido. Por favor, intente más tarde.");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error de comunicación con el servidor. Por favor, intente más tarde.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ha ocurrido un error inesperado: {ex.Message}");
                    // Log de depuración
                    System.Diagnostics.Debug.WriteLine("Excepción: " + ex.Message);
                }
            }

            return View(model);
        }
        
        public ActionResult Logoutprof()
        {
            SesionProf.CerrarSesionprof();
            return RedirectToAction("Login2");
        }
        public ActionResult Logoutadmin()
        {
            Sesionadmin.CerrarSesionadmin();
            return RedirectToAction("Login");
        }
        public ActionResult RegistroProf()
        {
            return View(new ProfesorViewModel());
        }
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarProfesor(ProfesorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegistroProf", model);
            }

            try
            {
                var req = new ReqInsertarProfe
                {
                    Profesor = new Profesores
                    {
                        nombre = model.nombre,
                        apellido1 = model.apellido1,
                        apellido2 = model.apellido2,
                        correo = model.correo,
                        usuario = model.usuario,
                        contra = model.contra,
                        estado = model.estado
                    }
                };

                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(req),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync("https://localhost:44345/api/admin/profesores", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResInsertarProfe>(responseContent);

                        if (res.respuesta)
                        {
                            TempData["SuccessMessage"] = "Profesor registrado exitosamente.";
                            return RedirectToAction("RegistroProf");  // Redirige a la vista correcta
                        }
                        else
                        {
                            if (res.listaDeErrores != null)
                            {
                                foreach (var error in res.listaDeErrores)
                                {
                                    ModelState.AddModelError("", error.error);
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error de servidor: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error inesperado.");
              
            }

            return View("RegistroProf",  model);
        }


        public ActionResult RegistroAlumno(AlumnoViewModel model)
        {
            
            return View(new AlumnoViewModel()); 
        }

        public async Task<ActionResult> RegistrarAlumno(AlumnoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegistroAlumno", model);
            }

            try
            {
                var req = new ReqInsertarAlumno
                {
                    alumno = new Alumno
                    {
                        nombre = model.Nombre,
                        apellido1 = model.Apellido1,
                        apellido2 = model.Apellido2,
                        numero_Cedula = model.NumeroCedula,
                        contacto = model.Contacto,
                        Grupo = model.Grupo,
                        Grado = model.Grado
                    }
                };

                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(req),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync("https://localhost:44345/api/admin/alumnos", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResIngresarAlumno>(responseContent);

                        if (res.respuesta)
                        {
                            TempData["SuccessMessage"] = "Alumno registrado exitosamente.";
                            return View("RegistroAlumno", new AlumnoViewModel());  // Devuelve un modelo vacío para reiniciar el formulario
                        }
                        else
                        {
                            if (res.listaDeErrores != null)
                            {
                                foreach (var error in res.listaDeErrores)
                                {
                                    ModelState.AddModelError("", error.error);
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error de servidor: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error inesperado.");
                // Aquí podrías loguear el error para analizarlo mejor
                // Por ejemplo: Logger.Error(ex); 
            }

            return View("RegistroAlumno", model);
        }
        public ActionResult Registromateria(MateriaViewModel model)
        {
            // Crear datos de ejemplo para el dashboard
            return View(new MateriaViewModel());
        }

        public async Task<ActionResult> Registrodemateria(MateriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegistroMateria", model);
            }

            try
            {
                var req = new ReqInsertarMateria
                {
                    materia = new Materia
                    {
                        usuarioprofe = model.Usuarioprofe,
                        Nombre = model.Nombre,
                        Grado = model.Grado,
                        Grupo1 = model.Grupo1,
                        DiaSemana = model.DiaSemana,
                        HoraInicio = model.HoraInicio,
                        HoraFin = model.HoraFin
                    }
                };

                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(
                        JsonConvert.SerializeObject(req),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync("https://localhost:44345/api/admin/materia", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResIngresarMateria>(responseContent);

                        if (res.respuesta)
                        {
                            TempData["SuccessMessage"] = "Alumno registrado exitosamente.";
                            return View("RegistroMateria", new MateriaViewModel());  // Devuelve un modelo vacío para reiniciar el formulario
                        }
                        else
                        {
                            if (res.listaDeErrores != null)
                            {
                                foreach (var error in res.listaDeErrores)
                                {
                                    ModelState.AddModelError("", error.error);
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Error de servidor: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error inesperado.");
               
            }

            return View("RegistroMateria", model);
        }


    }

    
}
    

    
