using Data.ControlCenter.Model;
using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using webapiapp.Models;

namespace webapiapp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapConfig();
        }
        void MapConfig()
        {
          

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.LogActualizacion, LogActualizacionDTO>();
            AutoMapper.Mapper.CreateMap<LogActualizacionDTO, Data.ControlCenter.Model.LogActualizacion>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.AccountUser, AccountUserDTO>();
            AutoMapper.Mapper.CreateMap<AccountUserDTO, Data.ControlCenter.Model.AccountUser>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Patients, PatientsDTO>();
            AutoMapper.Mapper.CreateMap<PatientsDTO, Data.ControlCenter.Model.Patients>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Medic, MedicDTO>();
            AutoMapper.Mapper.CreateMap<MedicDTO, Data.ControlCenter.Model.Medic>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Appointments_medical, Appointments_medicalDTO>();
            AutoMapper.Mapper.CreateMap<Appointments_medicalDTO, Data.ControlCenter.Model.Appointments_medical>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Appointments_attached, Appointments_attachedDTO>();
            AutoMapper.Mapper.CreateMap<Appointments_attachedDTO, Data.ControlCenter.Model.Appointments_attached>();

        }
    }
}