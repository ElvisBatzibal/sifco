using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace webapiapp.Models
{
    [Serializable()]
    [JsonObject]
    public class Appointments_medicalDTO
    {        /// <summary>
             /// Id
             /// </summary>
        public int EntityID { get; set; }
        public int IdPatient { get; set; }
        public int IdMedic { get; set; }
        public DateTime Scheduleddate { get; set; }
        public string Scheduledhour { get; set; }
        public string Scheduledhourend { get; set; }
        public DateTime RequestDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public nStatus Status { get; set; }
        public string Comments { get; set; }
        public string Medicalservice { get; set; }
        public string Address { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public MedicDTO Medic { get; set; }
        public PatientsDTO Patient { get; set; }

        public List<Appointments_attachedDTO> Attacheds { get; set; }
        public Appointments_medicalDTO()
        {
            Status = nStatus.Pendiente;
            Log = new LogActualizacionDTO();
            Medic = new MedicDTO();
            Patient = new PatientsDTO();
            Attacheds = new List<Appointments_attachedDTO>();
        }
    }

    public enum nStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "Enprogreso")]
        Enprogreso,
        [EnumMember(Value = "Finalizada")]
        Finalizada,
        [EnumMember(Value = "Cancelada")]
        Cancelada
    }
}