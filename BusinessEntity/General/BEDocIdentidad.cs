using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entity.BusinessEntity.General
{
   public class BEDocIdentidad
    {
        [BEColumn ("IDDocumento")]
        public int pIDDocumento { get; set; }

        [BEColumn("NameDocumento")]
        public string pNameDocumento { get; set; }
    }
}
