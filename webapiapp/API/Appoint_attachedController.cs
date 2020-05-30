using Data.ControlCenter;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapiapp.Models;

namespace webapiapp.API
{
    [EnableCorsAttribute("*", "*", "*")]
    public class Appoint_attachedController : ApiController
    {
        #region Conection Data
        //Crear la sesion para trabajar los QUERIES
        public void NHibernateSessionClose() { SessionManager.Instance.CloseSession(); }
        private ISession NHibernateSession
        {
            get
            {
                return SessionManager.Instance.GetSession();
            }
        }
        #endregion
        private Data.ControlCenter.Repository.Appointments_attachedRepositorio uR = new Data.ControlCenter.Repository.Appointments_attachedRepositorio();


        [HttpPost]
        [Route("api/Appointments_attached/Create")]
        public ResponseDTO CrearRegistro([FromBody] Appointments_attachedDTO value)
        {
            var Response = new ResponseDTO();

            Data.ControlCenter.Model.Appointments_attached u = new Data.ControlCenter.Model.Appointments_attached();
            u = AutoMapper.Mapper.Map<Appointments_attachedDTO, Data.ControlCenter.Model.Appointments_attached>(value);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogCreacion(u.Log);
            uR.SaveOrUpdate(u);
            if (u.EntityID > 0)
            {
                Response.Success = true;
                Response.Message = "Creado";
            }
            return Response;
        }


        [HttpPost]
        [Route("api/Appointments_attached/GetID")]
        public Appointments_attachedDTO GetID([FromBody] Appointments_attachedDTO value)
        {
            Data.ControlCenter.Model.Appointments_attached u;
            Appointments_attachedDTO uDTO = new Appointments_attachedDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Appointments_attached, Appointments_attachedDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Appointments_attached/GetFileName")]
        public Appointments_attachedDTO GetFileName([FromBody] Appointments_attachedDTO value)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_attached>()
            .Where(de => de.FileName == value.FileName)
            .List();

            IList<Models.Appointments_attachedDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_attached>, IList<Models.Appointments_attachedDTO>>(query);

            var Reg = new Models.Appointments_attachedDTO();
            foreach (var item in cDTO)
            {
                Reg = item;

                break;
            }

            var ApiArchivo = new Class.UploadedFile();
            Reg.Content = ApiArchivo.GetFile(value.FileName);
            return Reg;
        }

        public List<Models.Appointments_attachedDTO> ListAnexoCita(int id)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_attached>()
            .Where(de => de.IdApp_medical == id)
            .List();

            IList<Models.Appointments_attachedDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_attached>, IList<Models.Appointments_attachedDTO>>(query);

            return cDTO.OrderByDescending(x => x.EntityID).ToList();
        }

    }
}
