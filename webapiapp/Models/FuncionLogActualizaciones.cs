using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;


namespace webapiapp.Models
{
    public class FuncionLogActualizaciones
    {
        public void LogCreacion(Data.ControlCenter.Model.LogActualizacion log)
        {
            log.IdHostCreation = f_ObtenerIp();
            log.CreationDate = DateTime.Now;
            log.UpdateDate = DateTime.Now;
            log.Active = true;
        }
        public LogActualizacionDTO ReturnLogCreacion(LogActualizacionDTO id = null)
        {
            LogActualizacionDTO log = new LogActualizacionDTO();
            log.IdHostCreation = f_ObtenerIp();
            log.CreationDate = DateTime.Now;
            log.UpdateDate = DateTime.Now;
            log.Active = true;

            return log;
        }

        public void LogModificacion(Data.ControlCenter.Model.LogActualizacion log)
        {
            log.IdHostCreation = f_ObtenerIp();
            log.CreationDate = DateTime.Now;
            log.UpdateDate = DateTime.Now;
            log.Active = true;
        }

        public void LogEliminacion(Data.ControlCenter.Model.LogActualizacion log)
        {
            log.IdHostCreation = f_ObtenerIp();
            log.CreationDate = DateTime.Now;
            log.UpdateDate = DateTime.Now;
            log.Active = false;
        }

        public string f_ObtenerIp()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}