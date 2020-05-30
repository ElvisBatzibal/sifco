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
    public class MedicController : ApiController
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
        private Data.ControlCenter.Repository.MedicRepositorio uR = new Data.ControlCenter.Repository.MedicRepositorio();


        [HttpPost]
        [Route("api/Medic/Create")]
        public ResponseDTO CrearRegistro([FromBody] MedicDTO value)
        {
            var Response = new ResponseDTO();

            Data.ControlCenter.Model.Medic u = new Data.ControlCenter.Model.Medic();
            u = AutoMapper.Mapper.Map<MedicDTO, Data.ControlCenter.Model.Medic>(value);

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
        [Route("api/Medic/Update")]
        public ResponseDTO UpdateRegistro([FromBody] MedicDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.Medic u;

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
        [Route("api/Medic/GetID")]
        public MedicDTO GetID([FromBody] MedicDTO value)
        {
            Data.ControlCenter.Model.Medic u;
            MedicDTO uDTO = new MedicDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Medic, MedicDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Medic/GetAll")]
        public IList<MedicDTO> GetAll()
        {
            IList<Data.ControlCenter.Model.Medic> u;
            IList<MedicDTO> uDTO;
            try
            {
                u = uR.GetAll().ToList();
                uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Medic>, IList<MedicDTO>>(u);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return uDTO;
        }
    }
}
