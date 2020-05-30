using Data.ControlCenter.Model;
using Microsoft.Azure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;

namespace webapiapp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Page init 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["IdUsuario"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        /// <summary>
        /// Post login View
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            return RedirectToAction("Log", new { User = username, password = password });
        }
        public ActionResult LogOut(string username, string password)
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Funcion  de validacion de login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult Log(string user, string password)
        {
            if (String.IsNullOrEmpty(user))
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Ingrese usuario";
                return Index();
            }
            if (String.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Ingrese usuario";
                return Index();
            }
            API.AccountUserUserController loginController = new API.AccountUserUserController();

            var login = new Models.AccountUserDTO()
            {
                UserName = user,
                Password = password
            };

            var Respuesta = loginController.Login(login);

            //using (var client = new HttpClient())
            //{                
            //    client.BaseAddress = new Uri(Class.UrlRefererapi.UrlBaseAPI);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    Respuesta = client
            //        .PostAsJsonAsync("api/AccountUser/Login/", login)
            //        .Result
            //        .Content.ReadAsAsync<Models.LoginResponse>().Result;
            //}

            if (Respuesta.Success)
            {
                string mifecha = DateTime.Now.ToShortDateString();
                Session["Fecha"] = mifecha;
                Session["Usuario"] = Respuesta;
                Session["UsuarioNombre"] = Respuesta.UserName;
                Session["IdUsuario"] = Respuesta.EntityID;
                if (Respuesta.IdProfile == 1)
                {
                    return RedirectToAction("Index", "HomePatient");
                }
                else
                if (Respuesta.IdProfile == 2)
                {
                    return RedirectToAction("Index", "HomeMedic");

                }
                else
                {
                    return RedirectToAction("Home", "Home");
                }

            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = (String.IsNullOrEmpty(Respuesta.Message) ? "" : Respuesta.Message);
                return RedirectToAction("Index", "Home");
            }

        }


        /// <summary>
        /// Pagina principal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Home()
        {
            API.PatientsController ApiPaciente = new API.PatientsController();
            API.MedicController ApiMedico = new API.MedicController();

            ViewBag.Pacientes = ApiPaciente.GetAll();
            ViewBag.Medicos = ApiMedico.GetAll();

            return View();
        }
        [HttpPost]
        public JsonResult CrearPaciente(string paciente)
        {
            var NewObject = JsonConvert.DeserializeObject<Models.PatientsDTO>(paciente);
            var Respuesta = new Models.ResponseDTO();

            if (String.IsNullOrEmpty(NewObject.FullName))
            {
                Respuesta.Message = "Nombre completo requerido";
            }
            if (String.IsNullOrEmpty(NewObject.DPI))
            {
                Respuesta.Message = "DPI requerido";
            }
            if (NewObject.DPI.Length != 13)
            {
                Respuesta.Message = "DPI incompleto";
            }
            NewObject.Log = new Models.LogActualizacionDTO();
            NewObject.Log.UserCreation = (int)Session["IdUsuario"];

            try
            {
                API.PatientsController ApiPaciente = new API.PatientsController();
                Respuesta = ApiPaciente.CrearRegistro(NewObject);
            }
            catch (Exception ex)
            {
                Respuesta.Message = ex.Message;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CrearMedico(string medico)
        {
            var NewObject = JsonConvert.DeserializeObject<Models.MedicDTO>(medico);
            var Respuesta = new Models.ResponseDTO();

            if (String.IsNullOrEmpty(NewObject.FullName))
            {
                Respuesta.Message = "Nombre completo requerido";
            }
            if (String.IsNullOrEmpty(NewObject.DPI))
            {
                Respuesta.Message = "DPI requerido";
            }
            if (NewObject.DPI.Length != 13)
            {
                Respuesta.Message = "DPI incompleto";
            }
            NewObject.Log = new Models.LogActualizacionDTO();
            NewObject.Log.UserCreation = (int)Session["IdUsuario"];
            try
            {
                API.MedicController ApiPaciente = new API.MedicController();
                Respuesta = ApiPaciente.CrearRegistro(NewObject);
            }
            catch (Exception ex)
            {
                Respuesta.Message = ex.Message;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ShowCrearCita(string k)
        {
            API.MedicController ApiMedico = new API.MedicController();
            API.PatientsController ApiPaciente = new API.PatientsController();
            ViewBag.Medicos = ApiMedico.GetAll();
            ViewBag.Paciente = ApiPaciente.GetID(new Models.PatientsDTO { EntityID = int.Parse(k) });
            return PartialView("_CrearCita");
        }

        [HttpPost]
        public JsonResult CrearCita(string cita)
        {
            var NewObject = JsonConvert.DeserializeObject<Models.Appointments_medicalDTO>(cita);
            var Respuesta = new Models.ResponseDTO();

            NewObject.RequestDate = DateTime.Now;
            NewObject.Log = new Models.LogActualizacionDTO();
            NewObject.Log.UserCreation = (int)Session["IdUsuario"];
            try
            {
                API.Appoint_medicalController ApiPaciente = new API.Appoint_medicalController();
                Respuesta = ApiPaciente.CrearRegistro(NewObject);
            }
            catch (Exception ex)
            {
                Respuesta.Message = ex.Message;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowCitasPaciente(string k)
        {
            API.Appoint_medicalController ApiCitas = new API.Appoint_medicalController();

            ViewBag.ListaCitas = ApiCitas.ListIdPatient(int.Parse(k));
            return PartialView("_CitasPaciente");
        }

        [HttpPost]
        public ActionResult ShowCitasMedico(string k)
        {
            API.Appoint_medicalController ApiCitas = new API.Appoint_medicalController();

            ViewBag.ListaCitas = ApiCitas.ListIdMedico(int.Parse(k));
            return PartialView("_CitasMedico");
        }

        [HttpPost]
        public ActionResult ShowEditarCita(string k)
        {
            API.Appoint_medicalController ApiCitas = new API.Appoint_medicalController();

            ViewBag.Cita = ApiCitas.GetID(new Models.Appointments_medicalDTO { EntityID = int.Parse(k) });

            var TipodeEstado = Enum.GetValues(typeof(webapiapp.Models.nStatus)).Cast<webapiapp.Models.nStatus>().ToList();
            ViewBag.TipodeEstado = TipodeEstado;
            return PartialView("_EditarCita");
        }
        [HttpPost]
        public JsonResult EditarCita(string cita)
        {
            var NewObject = JsonConvert.DeserializeObject<Models.Appointments_medicalDTO>(cita);
            var Respuesta = new Models.ResponseDTO();

            NewObject.RequestDate = DateTime.Now;
            NewObject.Log = new Models.LogActualizacionDTO();
            NewObject.Log.UserUpdate = (int)Session["IdUsuario"];
            try
            {
                API.Appoint_medicalController ApiPaciente = new API.Appoint_medicalController();
                Respuesta = ApiPaciente.UpdateRegistro(NewObject);
            }
            catch (Exception ex)
            {
                Respuesta.Message = ex.Message;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowAddAnexo(int k)
        {
            Session["IdApp_medical"] = k;

            return PartialView("_AddAnexo");
        }

        [HttpPost]
        public JsonResult AddAnexo(FormCollection frm)
        {
            var respuesta = new Models.ResponseDTO();
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase uploadedFile = Request.Files[0];
                if (!String.IsNullOrEmpty(uploadedFile.FileName))
                {
                    if (uploadedFile != null && uploadedFile.ContentLength > 0)
                    {
                        var FileExtension = Path.GetExtension(uploadedFile.FileName);
                        var ContentType = uploadedFile.ContentType;

                        var namefile = String.Empty;
                        var FileUpload = new Models.Appointments_attachedDTO();
                        try
                        {
                            FileUpload.ContentType = ContentType;
                            FileUpload.FileType = FileExtension;
                            var array = new Byte[uploadedFile.ContentLength];
                            uploadedFile.InputStream.Position = 0;
                            uploadedFile.InputStream.Read(array, 0, uploadedFile.ContentLength);
                            FileUpload.Content = array;
                            respuesta = new Class.UploadedFile().AddFile(FileUpload);
                            //namefile = await Class.StorageService.Instance.UploadFile(uploadedFile.InputStream, FileExtension).;
                        }
                        catch (Exception ex)
                        {

                            respuesta.Message = ex.Message;
                        }
                        if (respuesta.Success == true)
                        {
                            Models.Appointments_attachedDTO newAttchment = new Models.Appointments_attachedDTO();
                            //newAttchment.UrlFile = "https://appbatzstorage.blob.core.windows.net/appbatzimages/" + namefile;
                            newAttchment.FileName = respuesta.InternalKey;
                            var IdApp_medical = (int)Session["IdApp_medical"];
                            newAttchment.IdApp_medical = IdApp_medical;
                            newAttchment.Log = new Models.LogActualizacionDTO();
                            newAttchment.Log.UserCreation = (int)Session["IdUsuario"];
                            newAttchment.FileType = FileExtension;
                            newAttchment.ContentType = ContentType;
                            API.Appoint_attachedController ApiAnexo = new API.Appoint_attachedController();
                            respuesta = ApiAnexo.CrearRegistro(newAttchment);
                        }

                    }
                    else
                    {
                        respuesta.Message = "Tamaño de archivo inválido";
                    }
                }
                else
                {
                    respuesta.Message = "Nombre de archivo vacio";
                }
            }
            else
            {
                respuesta.Message = "No existe un documento de adjunto";
            }
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowListarAnexo(int k)
        {
            API.Appoint_attachedController ApiAnexo = new API.Appoint_attachedController();
            ViewBag.ListaAnexo = ApiAnexo.ListAnexoCita(k);

            return PartialView("_ListarAnexo");
        }
        [HttpGet]
        public ActionResult OpenAnexo(string k)
        {

            var _File = new Models.Appointments_attachedDTO();
            API.Appoint_attachedController ApiAnexo = new API.Appoint_attachedController();
            _File = ApiAnexo.GetFileName(new Models.Appointments_attachedDTO { FileName = k });

            Stream stream = new MemoryStream(_File.Content);
            stream.Flush();
            return new FileStreamResult(stream, _File.ContentType);
        }

        [HttpPost]
        public ActionResult ShowCrearUser(int k, string j, string name = "")
        {
            ViewBag.Id = k;
            ViewBag.Perfil = j;
            ViewBag.UserName = name;
            return PartialView("_CrearUser");
        }
        [HttpPost]
        public JsonResult CrearUsuario(string usuario)
        {
            var NewObject = JsonConvert.DeserializeObject<Models.AccountUserDTO>(usuario);
            var Respuesta = new Models.ResponseDTO();


            NewObject.Log = new Models.LogActualizacionDTO();
            NewObject.Log.Active = true;
            NewObject.Log.UserUpdate = (int)Session["IdUsuario"];
            NewObject.Log.UserCreation = (int)Session["IdUsuario"];
            try
            {
                API.AccountUserUserController ApiUser = new API.AccountUserUserController();
                Respuesta = ApiUser.CrearRegistro(NewObject);
            }
            catch (Exception ex)
            {
                Respuesta.Message = ex.Message;
            }
            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

    }
}
