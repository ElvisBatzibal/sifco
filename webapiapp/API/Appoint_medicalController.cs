using Data.ControlCenter;
using Data.ControlCenter.Model;
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
    public class Appoint_medicalController : ApiController
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
        private Data.ControlCenter.Repository.Appointments_medicalRepositorio uR = new Data.ControlCenter.Repository.Appointments_medicalRepositorio();


        [HttpPost]
        [Route("api/Appointments_medical/Create")]
        public ResponseDTO CrearRegistro([FromBody] Appointments_medicalDTO value)
        {
            var Response = new ResponseDTO();

            Data.ControlCenter.Model.Appointments_medical u = new Data.ControlCenter.Model.Appointments_medical();
            u = AutoMapper.Mapper.Map<Appointments_medicalDTO, Data.ControlCenter.Model.Appointments_medical>(value);

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
        public ResponseDTO UpdateRegistro([FromBody] Appointments_medicalDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.Appointments_medical u;

            u = uR.GetById(value.EntityID, false);
            //u = AutoMapper.Mapper.Map(value, u);
            u.Scheduledhourend = value.Scheduledhourend;
            u.Status = (Data.ControlCenter.Model.nStatus)Enum.Parse(typeof(Data.ControlCenter.Model.nStatus), value.Status.ToString());
            u.Comments = value.Comments;
            u.Medicalservice = value.Medicalservice;
            u.Address = value.Address;
            u.Log.UserUpdate = value.Log.UserUpdate;
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
        public Appointments_medicalDTO GetID([FromBody] Appointments_medicalDTO value)
        {
            Data.ControlCenter.Model.Appointments_medical u;
            Appointments_medicalDTO uDTO = new Appointments_medicalDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Appointments_medical, Appointments_medicalDTO>(u);

            PatientsController ApiPaciente = new PatientsController();
            uDTO.Patient = ApiPaciente.GetID(new PatientsDTO { EntityID = uDTO.IdPatient });
            MedicController ApiMedico = new MedicController();
            uDTO.Medic = ApiMedico.GetID(new MedicDTO { EntityID = uDTO.IdPatient });
            return uDTO;
        }
        [HttpPost]
        [Route("api/Medic/GetAll")]
        public IList<Appointments_medicalDTO> GetAll()
        {
            IList<Data.ControlCenter.Model.Appointments_medical> u;
            IList<Appointments_medicalDTO> uDTO;
            try
            {
                u = uR.GetAll().ToList();
                uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_medical>, IList<Appointments_medicalDTO>>(u);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return uDTO;
        }

        public List<Models.Appointments_medicalDTO> ListIdPatient(int id)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_medical>()
            .Where(de => de.IdPatient == id)
            .List();

            IList<Models.Appointments_medicalDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_medical>, IList<Models.Appointments_medicalDTO>>(query);

            API.MedicController ApiMedico = new MedicController();
            foreach (var item in cDTO)
            {
                item.Medic = ApiMedico.GetID(new MedicDTO { EntityID = item.IdMedic });
            }
            return cDTO.OrderByDescending(x => x.EntityID).ToList();
        }
        public List<Models.Appointments_medicalDTO> ListByIdUser(int id)
        {

            var query1 = NHibernateSession.QueryOver<Data.ControlCenter.Model.Patients>()
            .Where(de => de.IdAccountUser == id)
            .List();

            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_medical>()
            .Where(de => de.IdPatient == query1.First().EntityID)
            .List();

            IList<Models.Appointments_medicalDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_medical>, IList<Models.Appointments_medicalDTO>>(query);

            API.MedicController ApiMedico = new MedicController();
            foreach (var item in cDTO)
            {
                item.Medic = ApiMedico.GetID(new MedicDTO { EntityID = item.IdMedic });
            }
            return cDTO.OrderByDescending(x => x.EntityID).ToList();
        }


        public List<Models.Appointments_medicalDTO> ListIdMedico(int id)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_medical>()
            .Where(de => de.IdMedic == id)
            .List();

            IList<Models.Appointments_medicalDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_medical>, IList<Models.Appointments_medicalDTO>>(query);

            API.PatientsController ApiPaciente = new PatientsController();
            foreach (var item in cDTO)
            {
                item.Patient = ApiPaciente.GetID(new PatientsDTO { EntityID = item.IdPatient });
            }
            return cDTO.OrderByDescending(x => x.EntityID).ToList();
        }
        public List<Models.Appointments_medicalDTO> ListByIdUserMedic(int id)
        {
            var query1 = NHibernateSession.QueryOver<Data.ControlCenter.Model.Medic>()
            .Where(de => de.IdAccountUser == id)
            .List();

            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Appointments_medical>()
            .Where(de => de.IdMedic == query1.First().EntityID)
            .List();

            IList<Models.Appointments_medicalDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Appointments_medical>, IList<Models.Appointments_medicalDTO>>(query);

            API.PatientsController ApiPaciente = new PatientsController();
            foreach (var item in cDTO)
            {
                item.Patient = ApiPaciente.GetID(new PatientsDTO { EntityID = item.IdPatient });
            }
            return cDTO.OrderByDescending(x => x.EntityID).ToList();
        }
    }
}
