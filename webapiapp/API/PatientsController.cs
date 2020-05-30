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
    public class PatientsController : ApiController
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
        private Data.ControlCenter.Repository.PatientsRepositorio uR = new Data.ControlCenter.Repository.PatientsRepositorio();
             

        [HttpPost]
        [Route("api/Patients/Create")]
        public ResponseDTO CrearRegistro([FromBody] PatientsDTO value)
        {
            var Response = new ResponseDTO();

            Data.ControlCenter.Model.Patients u = new Data.ControlCenter.Model.Patients();
            u = AutoMapper.Mapper.Map<PatientsDTO, Data.ControlCenter.Model.Patients>(value);

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
        [Route("api/Patients/Update")]
        public ResponseDTO UpdateRegistro([FromBody] PatientsDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.Patients u;

            u = uR.GetById(value.EntityID, false);
            u = AutoMapper.Mapper.Map(value, u);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogModificacion(u.Log);

            uR.SaveOrUpdate(u);
            uR.CommitChanges();

            Response.Success = true;
            Response.Message = "Actualizado";

            return Response;
        }

        [HttpPost]
        [Route("api/Patients/GetID")]
        public PatientsDTO GetID([FromBody] PatientsDTO value)
        {
            Data.ControlCenter.Model.Patients u;
            PatientsDTO uDTO = new PatientsDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Patients, PatientsDTO>(u);

            return uDTO;
        }
   
        [HttpPost]
        [Route("api/Patients/GetAll")]
        public IList<PatientsDTO> GetAll()
        {
            IList<Data.ControlCenter.Model.Patients> u;
            IList<PatientsDTO> uDTO;
            try
            {

                u = uR.GetAll().ToList();

                uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Patients>, IList<PatientsDTO>>(u);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return uDTO;
        }
    }
}
