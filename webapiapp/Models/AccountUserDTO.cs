using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    [JsonObject]
    public class AccountUserDTO: ResponseDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int EntityID { get; set; }
        public int IdProfile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public LogActualizacionDTO Log { get; set; }      
        public AccountUserDTO()
        {
            Log = new LogActualizacionDTO();
        }
    }
}