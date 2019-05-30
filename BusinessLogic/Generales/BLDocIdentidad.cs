using Facturacion.Entity.BusinessEntity.General;
using Facturacion.Entity.DataAccess.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entity.BusinessLogic.Generales
{
    public class BLDocIdentidad
    {
        DADocIdentidad objDA = new DADocIdentidad();

        public List<BEDocIdentidad> listaDocIdentidadBL()
        {
            return objDA.ListaDocIdentidadDA();
        }

        public string INS_DocIdentidadBL(BEDocIdentidad objBE)
        {
            return objDA.INSDocIdentidad(objBE);
        }

        public string UPD_DocIdentidadBL(BEDocIdentidad objBE)
        {
            return objDA.DLTDocIdentidadDA(objBE);
        }
    }
}
