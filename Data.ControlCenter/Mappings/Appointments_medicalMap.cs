using Data.ControlCenter.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
    public class Appointments_medicalMap : ClassMap<Appointments_medical>
    {
        /// <summary>
        /// Mapeo  Modelo Usuario en carpeta Model\Usuario.cs
        /// </summary>
        public Appointments_medicalMap()
        {

            Id(x => x.EntityID).Column("Id").GeneratedBy.Identity();
            Map(x => x.IdPatient);
            Map(x => x.IdMedic);
            Map(x => x.Scheduleddate);
            Map(x => x.Scheduledhour);
            Map(x => x.Scheduledhourend);
            Map(x => x.RequestDate);
            Map(x => x.Status);
            Map(x => x.Comments);
            Map(x => x.Medicalservice);
            Map(x => x.Address);

            Component(x => x.Log, m =>//Mapeo componente LogActualizacion en carpeta Model\Usuario.cs
            {
                m.Map(x => x.Active);
                m.Map(x => x.CreationDate);
                m.Map(x => x.UpdateDate);
                m.Map(x => x.IdHostCreation);
               // m.Map(x => x.IdHostUpdate);
                m.Map(x => x.UserCreation);                
                m.Map(x => x.UserUpdate);               
                
            });
        }
    }
}
