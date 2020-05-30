using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.ControlCenter.Model;


using System.Net;

namespace DataConfig.Class
{
    public class Objetos
    {
        LogActualizacion CreacionLog()
        {
            LogActualizacion Log = new LogActualizacion();
            Log.IdHostCreation = f_ObtenerIp();
            Log.CreationDate = DateTime.Now;
            Log.UpdateDate = DateTime.Now;
            Log.UserCreation = 0;
            Log.UserUpdate = 0;
            return Log;
        }

        //public Rol CrearRolAdmin()
        //{
        //    Rol r = new Rol();
        //    r.Log = CreacionLog();
        //    r.NombreRol = "Administrador";
        //    return r;
        //}

        //public Usuario CrearUsuario()
        //{
        //    Usuario u = new Usuario();
        //    u.Log = CreacionLog();
        //    u.NombreUsuario = "Admin";
        //    u.Contraseña = "123456";
        //    u.newC = false;
            
        //    return u;
        //}

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
