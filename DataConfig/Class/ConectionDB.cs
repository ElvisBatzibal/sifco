using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBusinessConfig.Class
{

    public class ConectionDB
    {
        public string NombreDB { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string UserDB { get; set; }

        public ConectionDB()
        {
            Server = "DESKTOP-BMF8NFG";
            NombreDB = "SIFCO";
            UserDB = "sifco";
            Password = "sifco123456";
        }
    }
}

