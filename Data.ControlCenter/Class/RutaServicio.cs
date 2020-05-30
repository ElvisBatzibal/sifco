using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Class
{


    public class RutaServicio
    {
        /// <summary>
        /// Configuracion de conection de base de datos
        /// </summary>
        /// <returns></returns>
        public string BdConexionDBAzureManagementApp()
        {
            var connectionString = "Server=DESKTOP-BMF8NFG\\MSSQLSERVER02;Database=SIFCO_DEV;User Id=DEV;Password=123456";        
            
            return connectionString;
        } 

    }
}
