using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    [Serializable()]
    public class UploadDTO
    {

        public byte[] Content { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string FileType { get; set; }

        public UploadDTO()
        {

        }
    }
}