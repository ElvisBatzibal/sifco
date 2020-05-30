using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class Profile : DomainObject<int>
    {
        public virtual string Name { get; set; }

        public virtual LogActualizacion Log { get; set; }

        public Profile()
        {
            Log = new LogActualizacion();
        }
    }

}
