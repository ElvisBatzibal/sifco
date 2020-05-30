using Newtonsoft.Json;
using NHibernate.Hql.Ast.ANTLR.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Models
{
    [JsonObject]
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InternalKey { get; set; }

        public ResponseDTO()
        {
            Success = false;
        }

    }
}