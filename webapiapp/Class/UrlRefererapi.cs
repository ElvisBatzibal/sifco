using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiapp.Class
{
    public static class UrlRefererapi
    {
        public static string UrlBaseAPI = "https://institucion.azurewebsites.net/";

        public static string GetUrlApi()
        {
            //return "https://institucion.azurewebsites.net/";
            return "http://localhost:50346/";
        }
    }
}