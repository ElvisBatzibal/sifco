using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    public class Appointments_attachedDTO
    {        /// <summary>
             /// Id
             /// </summary>
        public int EntityID { get; set; }
        public int IdApp_medical { get; set; }
        public string FileName { get; set; }
        public string UrlFile { get; set; }
        public string ContentType { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        public LogActualizacionDTO Log { get; set; }
        public Appointments_attachedDTO()
        {
            Log = new LogActualizacionDTO();
            UrlFile = String.Empty;
        }
    }
}