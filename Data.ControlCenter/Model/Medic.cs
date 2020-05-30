using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class Medic : DomainObject<int>
    {
        public virtual string FullName { get; set; }
        public virtual DateTime Birthdate { get; set; }
        public virtual nGender Gender { get; set; }
        public virtual string DPI { get; set; }
        public virtual LogActualizacion Log { get; set; }
        public virtual int RegistrationNumber { get; set; }
        public virtual string Specialty { get; set; }
        public virtual int IdAccountUser { get; set; }
    }
}
