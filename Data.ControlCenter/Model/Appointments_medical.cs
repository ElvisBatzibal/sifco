using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    public class Appointments_medical: DomainObject<int>
    {
        public virtual int IdPatient { get; set; }
        public virtual int IdMedic { get; set; }
        public virtual DateTime Scheduleddate { get; set; }
        public virtual string Scheduledhour { get; set; }
        public virtual string Scheduledhourend { get; set; }
        public virtual DateTime RequestDate { get; set; }               
        public virtual nStatus Status { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Medicalservice { get; set; }
        public virtual string Address { get; set; }
        public virtual LogActualizacion Log { get; set; }
    }
    public enum nStatus
    {       
        Pendiente,      
        Enprogreso,       
        Finalizada,       
        Cancelada
    }
}
