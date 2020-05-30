using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    [Serializable()]
    [JsonObject]
    public class ProfileDTO
    {
        public int EntityID { get; set; }
        public string Name { get; set; }

        public LogActualizacionDTO Log { get; set; }
        public ProfileDTO()
        {
            Log = new LogActualizacionDTO();
        }
    }
}