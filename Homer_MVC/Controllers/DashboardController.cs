using Homer_MVC.Models;
using Homer_MVC.Models.Entidades;
using Homer_MVC.Models.Entidades.Request;
using Homer_MVC.Models.Entidades.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Homer_MVC.Controllers
{
    public class DashboardController : Controller
    {

        // Acción que maneja la vista principal del dashboard
        public ActionResult Index()
        {
            // Crear datos de ejemplo para el dashboard
            var model = new DashboardModel
            {

            };

            return View(model);  // Pasamos el modelo a la vista
        }
        public ActionResult menuprofesor()
        {
            // Crear datos de ejemplo para el dashboard
            var model = new ObtenergruposModel
            {

            };

            return View(model);  // Pasamos el modelo a la vista
        }
        public ActionResult menuprofe()
        {
            // Crear datos de ejemplo para el dashboard
            var model = new SeleccionarDiaViewModel
            {

            };

            return View(model);  // Pasamos el modelo a la vista
        }
        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> menuprofe2()
        {
            // Prepare the model first
            ObtenergruposModel model = new ObtenergruposModel();
            model.materias = new List<Materia>();

            try
            {
                // Populate professor name
                ViewBag.nombreprofe = SesionProf.NOMBRE + " " + SesionProf.APELLIDO1 + " " + SesionProf.APELLIDO2;
                ViewBag.dia = SeleccionGrupo.DiaSemana;

                ReqObtenerMateria req = new ReqObtenerMateria
                {
                    idprofe = SesionProf.id_sesionprof,
                    dia = SeleccionGrupo.DiaSemana
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/ObtenerGrupo", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResObtenerGrupo>(responseContent);

                        if (res.respuesta)
                        {
                            // Clear any previous data
                            model.materias.Clear();

                            // Populate materias
                            foreach (var item in res.grupos)
                            {
                                Materia unamateria = new Materia
                                {
                                    Id_Profe = item.Id_Profe,
                                    Id_Materia = item.Id_Materia,
                                    usuarioprofe = item.usuarioprofe,
                                    Id_Grupo = item.Id_Grupo,
                                    Id_Horario = item.Id_Horario,
                                    Grado = item.Grado,
                                    Grupo1 = item.Grupo1,
                                    Nombre = item.Nombre,
                                    DiaSemana = item.DiaSemana,
                                    HoraFin = item.HoraFin,
                                    HoraInicio = item.HoraInicio
                                };
                                model.materias.Add(unamateria);
                            }

                            // IMPORTANT: Create SelectList and assign to ViewBag
                            ViewBag.Materias = new SelectList(
                                model.materias.Select(m => new
                                {
                                    m.Id_Materia,
                                    NombreCompleto = $"{m.Nombre} - {m.Grado} - {m.Grupo1}"
                                }).ToList(),
                                "Id_Materia",
                                "NombreCompleto"
                            );
                        }
                        else
                        {
                            // Handle errors
                            foreach (var error in res.listaDeErrores)
                            {
                                ModelState.AddModelError("", error.error);
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
                ModelState.AddModelError("", "Ha ocurrido un error inesperado. Por favor, intente más tarde.");
            }

            // Return the model along with the view
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> MenuProfe(SeleccionarDiaViewModel model)
        {
            ViewBag.nombre = $"{SesionProf.NOMBRE} {SesionProf.APELLIDO1} {SesionProf.APELLIDO2}";


            if (string.IsNullOrEmpty(model.DiaSemana))
            {
                ModelState.AddModelError("", "Debe seleccionar un día de la semana.");
                return View(model); // Regresar la vista si no se seleccionó día
            }

            ReqObtenerMateria req = new ReqObtenerMateria
            {
                idprofe = SesionProf.id_sesionprof,
                dia = model.DiaSemana // Asignar el día seleccionado
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/ObtenerGrupo", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResIngresarMateria>(responseContent);

                        if (res.respuesta)
                        {

                            SeleccionGrupo.DiaSemana = model.DiaSemana;

                            return RedirectToAction("menuprofe2");
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


            return RedirectToAction("menuprofe2");
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Selecciongrupo(int Id_Materia)
        {
            try
            {
                // Find the selected material from the previously populated list
                ReqObtenerMateria reqMateria = new ReqObtenerMateria
                {
                    idprofe = SesionProf.id_sesionprof,
                    dia = SeleccionGrupo.DiaSemana
                };
                var jsonMateriaContent = new StringContent(JsonConvert.SerializeObject(reqMateria), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    // First, get the groups again to ensure we have the latest data
                    var materiaResponse = await client.PostAsync("https://localhost:44345/api/Profe/ObtenerGrupo", jsonMateriaContent);

                    if (materiaResponse.IsSuccessStatusCode)
                    {
                        var materiaResponseContent = await materiaResponse.Content.ReadAsStringAsync();
                        var materiaRes = JsonConvert.DeserializeObject<ResObtenerGrupo>(materiaResponseContent);

                        // Find the selected group details
                        var selectedGrupo = materiaRes.grupos.FirstOrDefault(g => g.Id_Materia == Id_Materia);

                        if (selectedGrupo == null)
                        {
                            ModelState.AddModelError("", "Grupo no encontrado");
                            return View("menuprofe2");
                        }

                        // Store the group ID in the static SeleccionGrupo class
                        SeleccionGrupo.idGrupo = selectedGrupo.Id_Grupo;

                        // Optional: Store additional group information if needed
                        SeleccionGrupo.materia = selectedGrupo.Nombre;


                        // Redirect to the next action
                        return RedirectToAction("Selecciongrupo");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al recuperar los grupos");
                        return View("menuprofe2");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if possible
                ModelState.AddModelError("", "Ha ocurrido un error inesperado");
                return View("menuprofe2");
            }
        }


        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> Selecciongrupo()
        {
            // Prepare the model first
            SelecionarGrupoViewModel model = new SelecionarGrupoViewModel();
            model.estudiantes = new List<Asistencia>();
            try
            {
                // Debug: Log the values being sent
                System.Diagnostics.Debug.WriteLine($"ID Profesor: {SesionProf.id_sesionprof}");
                System.Diagnostics.Debug.WriteLine($"ID Grupo: {SeleccionGrupo.idGrupo}");
                System.Diagnostics.Debug.WriteLine($"Día: {SeleccionGrupo.DiaSemana}");
                System.Diagnostics.Debug.WriteLine($"Materia: {SeleccionGrupo.materia}");

                // Populate professor name
                ViewBag.nombreprofe = SesionProf.NOMBRE + " " + SesionProf.APELLIDO1 + " " + SesionProf.APELLIDO2;
                ViewBag.dia = SeleccionGrupo.DiaSemana;
                ViewBag.materia = SeleccionGrupo.materia;

                ReqSeleccionarGrupo req = new ReqSeleccionarGrupo
                {
                    idProfe = (int)SesionProf.id_sesionprof,
                    idGrupo = (int)SeleccionGrupo.idGrupo,
                    dia = SeleccionGrupo.DiaSemana,
                    materia = SeleccionGrupo.materia
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/estudiantes", jsonContent);

                    // Debug: Log the response status and content
                    System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        // Debug: Log the raw response content
                        System.Diagnostics.Debug.WriteLine($"Response Content: {responseContent}");

                        var res = JsonConvert.DeserializeObject<ResSeleccionarGrupo>(responseContent);

                        // Debug: Log deserialization details
                        System.Diagnostics.Debug.WriteLine($"Respuesta: {res?.respuesta}");
                        System.Diagnostics.Debug.WriteLine($"Alumnos Count: {res?.estudiantes?.Count ?? 0}");

                        if (res.respuesta)
                        {
                            // Clear any previous data
                            model.estudiantes.Clear();

                            // Populate alumnos
                            foreach (var item in res.estudiantes)
                            {
                                Asistencia unalumno = new Asistencia
                                {
                                    id_Alumno = item.id_Alumno,
                                    nombre_Alumno = item.nombre_Alumno,
                                    apellido1 = item.apellido1,
                                    apellido2 = item.apellido2,

                                };
                                model.estudiantes.Add(unalumno);
                                Seleccionasistencia.asistencia = model.estudiantes;
                            }

                            ViewBag.alumnos = new SelectList(
                                model.estudiantes.Select(m => new
                                {
                                    m.id_Alumno,
                                    NombreCompleto = $"{m.nombre_Alumno} - {m.apellido1} - {m.apellido2}"
                                }).ToList(),
                                "id_Alumno",
                                "NombreCompleto"
                            );
                        }
                        else
                        {
                            // Handle errors
                            foreach (var error in res.listaDeErrores)
                            {
                                ModelState.AddModelError("", error.error);
                                System.Diagnostics.Debug.WriteLine($"Error: {error.error}");
                            }
                        }
                    }
                    else
                    {
                        // Debug: Log the error response
                        var errorContent = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Error Response: {errorContent}");

                        ModelState.AddModelError("", "Error de comunicación con el servidor. Por favor, intente más tarde.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Debug: Log the full exception
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                ModelState.AddModelError("", "Ha ocurrido un error inesperado. Por favor, intente más tarde.");
            }


            // Return the model along with the view
            return View(model);
        }


        public ActionResult SelecionarGrupo()  // o el nombre que tenga tu action
        {
            // Asumiendo que ya tienes los datos de los estudiantes en Seleccionasistencia.asistencia
            var viewModel = new PasarAsistenciaViewModel
            {
                ausencias = Seleccionasistencia.asistencia.Select(a => new Ausencia
                {
                    id_Alumno = a.id_Alumno,
                    nombre_Alumno = a.nombre_Alumno,
                    apellido1 = a.apellido1,
                    apellido2 = a.apellido2,
                    id_Grupo = a.id_Grupo,
                    materia = a.materia
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> PasarAsistencia(PasarAsistenciaViewModel model)
        {
            try
            {
                if (model.ausencias == null)
                {
                    ModelState.AddModelError("", "No se recibieron datos de asistencia");
                    return View(model);
                }

                // Crear la lista de asistencias para enviar al API
                var listaAusencias = model.ausencias.Select(a => new Asistencia
                {
                    id_Profe = SesionProf.id_sesionprof,
                    dia = SeleccionGrupo.DiaSemana,
                    id_Grupo = SeleccionGrupo.idGrupo,
                    materia = SeleccionGrupo.materia,
                    fecha = model.fecha,
                    estado = a.estado,
                    id_Alumno = a.id_Alumno,
                    nombre_Alumno = a.nombre_Alumno,
                    apellido1 = a.apellido1,
                    apellido2 = a.apellido2
                }).ToList();

                var req = new ReqSeleccionarAusencia { asistencias = listaAusencias };

                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/asistencia", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        SeleccionFechas.fecha = model.fecha.Value;
                        return RedirectToAction("PasarAsistencia");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al enviar los datos. Por favor, intente más tarde.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error inesperado: " + ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConsultarAusencia(ConsultarAsistenciaViewModel model)
        {
            try
            {
                if (model.fechaConsulta.HasValue) // Verificar si la fecha está presente
                {
                    ReqConsultarFecha req = new ReqConsultarFecha
                    {
                        idprofe = SesionProf.id_sesionprof,
                        fecha = model.fechaConsulta.Value, // Usar el valor de la fecha si es válida
                        idgrupo = SeleccionGrupo.idGrupo,
                        materia = SeleccionGrupo.materia
                    };

                    using (var client = new HttpClient())
                    {
                        var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("https://localhost:44345/api/Profe/obtenerAusencias", jsonContent);

                        if (response.IsSuccessStatusCode)
                        {
                            SeleccionFechas.fecha = model.fechaConsulta.Value;
                            return RedirectToAction("ConsultarAusencia");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error al enviar los datos. Por favor, intente más tarde.");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Por favor seleccione una fecha válida.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error inesperado: " + ex.Message);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> PasarAsistencia()
        {
            PasarAsistenciaViewModel model = new PasarAsistenciaViewModel();
            model.ausencias = new List<Ausencia>();

            try
            {
                ViewBag.nombreprofe = SesionProf.NOMBRE + " " + SesionProf.APELLIDO1 + " " + SesionProf.APELLIDO2;
                ViewBag.dia = SeleccionGrupo.DiaSemana;
                ViewBag.Fecha = SeleccionFechas.fecha;
                ViewBag.Materia = SeleccionGrupo.materia;

                ReqConsultarFecha req = new ReqConsultarFecha
                {
                    idprofe = SesionProf.id_sesionprof,
                    fecha = SeleccionFechas.fecha, // Utiliza la fecha guardada
                    idgrupo = SeleccionGrupo.idGrupo,
                    materia = SeleccionGrupo.materia
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/obtenerAusencias", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResConsultarFecha>(responseContent);

                        if (res.respuesta)
                        {
                            model.ausencias.Clear();
                            foreach (var item in res.ausencias)
                            {
                                model.ausencias.Add(new Ausencia
                                {
                                    id_Profe = item.id_Profe,
                                    id_Grupo = item.id_Grupo,
                                    id_Alumno = item.id_Alumno,
                                    id_Asistencia = item.id_Asistencia,
                                    nombre_Alumno = item.nombre_Alumno,
                                    apellido1 = item.apellido1,
                                    apellido2 = item.apellido2,
                                    estado = item.estado,
                                    fecha = item.fecha,
                                    materia = item.materia,
                                    dia = item.dia
                                });
                            }
                        }
                        else
                        {
                            foreach (var error in res.listaDeErrores)
                            {
                                ModelState.AddModelError("", error.error);
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
                ModelState.AddModelError("", "Ha ocurrido un error inesperado. Por favor, intente más tarde.");
            }

            return View(model);
        }



        [HttpGet]
        public async Task<ActionResult> ConsultarAusencia()
        {
            ConsultarAsistenciaViewModel model = new ConsultarAsistenciaViewModel();
            model.ausencias = new List<Ausencia>();

            try
            {
                ViewBag.nombreprofe = SesionProf.NOMBRE + " " + SesionProf.APELLIDO1 + " " + SesionProf.APELLIDO2;
                ViewBag.dia = SeleccionGrupo.DiaSemana;
                ViewBag.Fecha = SeleccionFechas.fecha;
                ViewBag.Materia = SeleccionGrupo.materia;

                ReqConsultarFecha req = new ReqConsultarFecha
                {
                    idprofe = SesionProf.id_sesionprof,
                    fecha = SeleccionFechas.fecha, // Utiliza la fecha guardada
                    idgrupo = SeleccionGrupo.idGrupo,
                    materia = SeleccionGrupo.materia
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:44345/api/Profe/obtenerAusencias", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<ResConsultarFecha>(responseContent);

                        if (res.respuesta)
                        {
                            model.ausencias.Clear();
                            foreach (var item in res.ausencias)
                            {
                                model.ausencias.Add(new Ausencia
                                {
                                    id_Profe = item.id_Profe,
                                    id_Grupo = item.id_Grupo,
                                    id_Alumno = item.id_Alumno,
                                    id_Asistencia = item.id_Asistencia,
                                    nombre_Alumno = item.nombre_Alumno,
                                    apellido1 = item.apellido1,
                                    apellido2 = item.apellido2,
                                    estado = item.estado,
                                    fecha = item.fecha,
                                    materia = item.materia,
                                    dia = item.dia
                                });
                            }
                        }
                        else
                        {
                            foreach (var error in res.listaDeErrores)
                            {
                                ModelState.AddModelError("", error.error);
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
                ModelState.AddModelError("", "Ha ocurrido un error inesperado. Por favor, intente más tarde.");
            }

            return View(model);
        }


        public ActionResult borrardatosgrupo()
        {
            SeleccionGrupo.Volversesiongrupo();
            return RedirectToAction("menuprofe");
        }

        public ActionResult borrardatosfecha()
        {
            Seleccionasistencia.Volversesionasistencia();
            SeleccionFechas.Volversesionfechas();
            return RedirectToAction("Selecciongrupo");
        }

    }
}