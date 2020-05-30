using NHibernate.Hql.Ast.ANTLR.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Model
{
    /// <summary>
    /// LogActualizacion clase General  es llamado en todos los Modelos (Carpeta Model) captura los datos de la Maquina como: Host,Fecha,Ip etc.
    /// </summary>
    public class LogActualizacion
    {

        public virtual string IdHostCreation { get; set; }
        public virtual string IdHostUpdate { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual int UserCreation { get; set; }
        public virtual int UserUpdate { get; set; }
        public virtual bool Active { get; set; }
        public LogActualizacion()
        {

        }
    }

}
