using Facturacion.Entity.BusinessEntity.General;
using Facturacion.Entity.BusinessLogic.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facturacion.UI.Controllers
{
    public class DocIdentidadController : Controller
    {
        BEDocIdentidad objBE = new BEDocIdentidad();
        BLDocIdentidad objBL = new BLDocIdentidad();
        // GET: DocIdentidad
        public ActionResult ListaTabla()
        {
            return View(objBL.listaDocIdentidadBL());
        }
        public ActionResult VistaNuevoDocumentoIdentidad()
        {
            return View();
        }

        public ActionResult RegistroDocIdentidad(int txtcodigo,string txtnombre)
        {
            objBE.pIDDocumento = txtcodigo;
            objBE.pNameDocumento = txtnombre;

            string mensaje = "";
            if (objBL.INS_DocIdentidadBL(objBE) == "OK")
            {
                mensaje= "<script language='javascript' type='text/javascript'>" + "alert('Registrado Correctamente');window.location.href=" +
                    "'/DocIdentidad/ListaTabla';</script>";
            }
            else
            {
                mensaje = "<script language='javascript' type='text/javascript'>" + "alert('Error Al Registrar');window.location.href=" +
                  "'/DocIdentidad/VistaNuevoDocumentoIdentidad';</script>";
            }
                  return Content(mensaje);
        }

        public ActionResult VistaUPD(int id)
        {
            
            return View();
        }

        
        public ActionResult DeleteDocIdentidad(int id)
        {

            string mensaje = "";
            
            if (objBL.UPD_DocIdentidadBL(objBE) == "OK")
            {
                mensaje = "<script language='javascript' type='text/javascript'>" + "alert('Eliminado Correctamente');window.location.href=" +
                                   "'/DocIdentidad/ListaTabla';</script>";
            }
            else
            {
                mensaje = "<script language='javascript' type='text/javascript'>" + "alert('Error Al Eliminar');window.location.href=" +
              "'/DocIdentidad/ListaTabla';</script>";
            }
            return Content(mensaje);
        }
        
    }
}