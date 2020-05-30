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
    public class PatientsDTO
    {
        public int EntityID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public nGender Gender { get; set; }
        public string DPI { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public int IdAccountUser { get; set; }
        public PatientsDTO()
        {
            Log = new LogActualizacionDTO();
            Birthdate = DateTime.Now;
        }
    }
    public enum nGender
    {
        [EnumMember(Value = "Femenino")]
        Femenino,
        [EnumMember(Value = "Masculino")]
        Masculino
    }
}