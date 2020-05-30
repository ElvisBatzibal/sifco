using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class Appointments_attached : DomainObject<int>
    {
        public virtual int IdApp_medical { get; set; }
        public virtual string FileName { get; set; }
        public virtual string UrlFile { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FileType { get; set; }
        public virtual LogActualizacion Log { get; set; }
    }
}
