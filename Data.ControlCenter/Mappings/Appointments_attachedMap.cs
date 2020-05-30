using Data.ControlCenter.Model;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
    public class Appointments_attachedMap : ClassMap<Appointments_attached>
    {
        /// <summary>
        /// Mapeo  Modelo Usuario en carpeta Model\Usuario.cs
        /// </summary>

        public Appointments_attachedMap()
        {

            Id(x => x.EntityID).Column("Id").GeneratedBy.Identity();
            Map(x => x.IdApp_medical);
            Map(x => x.FileName);
            Map(x => x.UrlFile);
            Map(x => x.ContentType);
            Map(x => x.FileType);

            Component(x => x.Log, m =>//Mapeo componente LogActualizacion en carpeta Model\Usuario.cs
            {
                m.Map(x => x.Active);
                m.Map(x => x.CreationDate);
                m.Map(x => x.UpdateDate);
                m.Map(x => x.IdHostCreation);
                //m.Map(x => x.IdHostUpdate);
                m.Map(x => x.UserCreation);
                m.Map(x => x.UserUpdate);

            });
        }
    }
}
