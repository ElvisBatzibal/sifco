using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    [Serializable()]
    [JsonObject]
    public class MedicDTO
    {
        public int EntityID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public nGender Gender { get; set; }
        public string DPI { get; set; }
        public int RegistrationNumber { get; set; }
        public string Specialty { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public int IdAccountUser { get; set; }
        public MedicDTO()
        {
            Log = new LogActualizacionDTO();
            Birthdate = DateTime.Now;
        }
    }
}