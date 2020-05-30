using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Data.ControlCenter.Model
{
    /// <summary>
    /// Estructura Modelo Usuario (Base de Datos PedidosEnLinea , Tabla Usuario)
    /// </summary>
    public class AccountUser : DomainObject<int>
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int IdProfile { get; set; }


        public virtual LogActualizacion Log { get; set; }

        public AccountUser()
        {
            Log = new LogActualizacion();
        }
    }

}
