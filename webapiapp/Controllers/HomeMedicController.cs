using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapiapp.Controllers
{
    public class HomeMedicController : Controller
    {
        // GET: HomeMedic
        public ActionResult Index()
        {
            API.Appoint_medicalController ApiCitas = new API.Appoint_medicalController();
            var IdUsuario = (int)Session["IdUsuario"];
            ViewBag.ListaCitas = ApiCitas.ListByIdUserMedic(IdUsuario);                   

            return View();
        }
    }
}