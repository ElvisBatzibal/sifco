using FluentNHibernate.Mapping;
using Data.ControlCenter.Model;

namespace Data.ControlCenter.Mappings
{
    public class AccountUserMap : ClassMap<AccountUser>
    {
        /// <summary>
        /// Mapeo  Modelo Usuario en carpeta Model\Usuario.cs
        /// </summary>
        public AccountUserMap()
        {

            Id(x => x.EntityID).Column("Id").GeneratedBy.Identity();
            Map(x => x.UserName).Unique();
            Map(x => x.Password);
            Map(x => x.IdProfile);

            Component(x => x.Log, m =>//Mapeo componente LogActualizacion en carpeta Model\Usuario.cs
            {
                m.Map(x => x.Active);
                m.Map(x => x.CreationDate);
                m.Map(x => x.UpdateDate);
                m.Map(x => x.IdHostCreation);
                m.Map(x => x.IdHostUpdate);
                m.Map(x => x.UserCreation);
                m.Map(x => x.UserUpdate);
            });
        }
    }
}
