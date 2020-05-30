using Data.ControlCenter.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Mappings
{
    public class MedicMap : ClassMap<Medic>
    {
        public MedicMap()
        {

            Id(x => x.EntityID).Column("AccountNum").GeneratedBy.Identity();
            Map(x => x.FullName);
            Map(x => x.Birthdate);
            Map(x => x.Gender);
            Map(x => x.DPI);
            Map(x => x.RegistrationNumber);
            Map(x => x.Specialty);
            Map(x => x.IdAccountUser);

            Component(x => x.Log, m =>
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
