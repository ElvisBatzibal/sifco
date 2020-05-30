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
    public class AccountUserUserController : ApiController
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
        private Data.ControlCenter.Repository.AccountUserRepositorio uR = new Data.ControlCenter.Repository.AccountUserRepositorio();

        [HttpPost]
        [Route("api/AccountUser/Login")]
        public AccountUserDTO Login([FromBody] AccountUserDTO value)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.AccountUser>()
            .Where(de => de.UserName == value.UserName)
            .Where(de => de.Password == value.Password)
            .List();
            IList<AccountUserDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.AccountUser>, IList<AccountUserDTO>>(query);
            var UserLog = new AccountUserDTO();
            foreach (var item in cDTO)
            {
                UserLog = item;
                UserLog.Success = true;
                UserLog.Message = "Login success";
                break;
            }
            NHibernateSessionClose();
            return UserLog;
        }

        [HttpPost]
        [Route("api/AccountUser/Create")]
        public ResponseDTO CrearRegistro([FromBody] AccountUserDTO value)
        {
            var Response = new ResponseDTO();
            var IdPersonal = value.EntityID;
            value.EntityID = 0;
            Data.ControlCenter.Model.AccountUser u = new Data.ControlCenter.Model.AccountUser();
            u = AutoMapper.Mapper.Map<AccountUserDTO, Data.ControlCenter.Model.AccountUser>(value);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogCreacion(u.Log);
            uR.SaveOrUpdate(u);
            if (u.EntityID > 0)
            {
                Response.Success = true;
                Response.Message = "Creado";

                switch (value.IdProfile)
                {
                    case 1:
                        {
                            API.PatientsController ApiUpdate = new PatientsController();
                            var oData = ApiUpdate.GetID(new PatientsDTO { EntityID = IdPersonal });
                            oData.IdAccountUser = u.EntityID;
                            ApiUpdate.UpdateRegistro(oData);
                            break;
                        }
                    case 2:
                        {
                            API.MedicController ApiUpdate = new MedicController();
                            var oData = ApiUpdate.GetID(new MedicDTO { EntityID = IdPersonal });
                            oData.IdAccountUser = u.EntityID;
                            ApiUpdate.UpdateRegistro(oData);
                            break;
                        }

                    case 3:
                        {
                            break;
                        }
                }
            }
            return Response;
        }

        [HttpPost]
        [Route("api/AccountUser/Update")]
        public ResponseDTO UpdateRegistro([FromBody] AccountUserDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.AccountUser u;

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
        [Route("api/AccountUser/GetCardCode")]
        public AccountUserDTO GetCardCode([FromBody] AccountUserDTO value)
        {
            Data.ControlCenter.Model.AccountUser u;
            AccountUserDTO uDTO = new AccountUserDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.AccountUser, AccountUserDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/AccountUser/GetAllCardCode")]
        public IList<AccountUserDTO> GetAllCardCode()
        {
            IList<Data.ControlCenter.Model.AccountUser> u;
            IList<AccountUserDTO> uDTO;
            try
            {

                u = uR.GetAll().ToList();

                uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.AccountUser>, IList<AccountUserDTO>>(u);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return uDTO;
        }
    }
}
