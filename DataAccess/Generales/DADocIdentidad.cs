using Facturacion.Entity.BusinessEntity.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entity.DataAccess.Generales
{
  public class DADocIdentidad : Conexion
    {
        SqlCommand cmdSQL = new SqlCommand();

        public List<BEDocIdentidad> ListaDocIdentidadDA()
        {
            List<BEDocIdentidad> objlista = new List<BEDocIdentidad>();
            cmdSQL.Connection = NewConnection;
            cmdSQL.CommandText = "USP_LST_DocIdentidad";
            cmdSQL.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = fLeer(cmdSQL);
            objlista = (List<BEDocIdentidad>)ConvertirDataReaderALista<BEDocIdentidad>(dr);

            return objlista;
        }

        public string INSDocIdentidad(BEDocIdentidad objBE)
        {
            string strResultado = "";
            try
            {
                cmdSQL.Connection = NewConnection;
                cmdSQL.CommandText = "USP_INS_DocIdentidad";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@IDDocumento", objBE.pIDDocumento == 0 ? 0 : objBE.pIDDocumento, DbType.Int32);
                pAddParameter(cmdSQL, "@NameDocumento", objBE.pNameDocumento == "" ? "" : objBE.pNameDocumento, DbType.String);
                strResultado = fEjecutar(cmdSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
              
            }
            return strResultado;
        }

        public string UPDDocIdentidad(BEDocIdentidad objBE)
        {
            string strResultado = "";
            try
            {
                cmdSQL.Connection = NewConnection;
                cmdSQL.CommandText = "USP_UPD_DocIdentidad";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@NameDocumento", objBE.pNameDocumento == "" ? "" : objBE.pNameDocumento, DbType.String);
                pAddParameter(cmdSQL, "@IDDocumento", objBE.pIDDocumento == 0 ? 0 : objBE.pIDDocumento, DbType.Int32);
                strResultado = fEjecutar(cmdSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }

            }
            return strResultado;
        }
        //

        public string DLTDocIdentidadDA(BEDocIdentidad objBE)
        {
            string strResultado = "";
            try
            {
                cmdSQL.Connection = NewConnection;
                cmdSQL.CommandText = "USP_DLT_DocIdentidad";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@IDDocumento", objBE.pIDDocumento == 0 ? 0 : objBE.pIDDocumento, DbType.Int32);
                strResultado = fEjecutar(cmdSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }

            }
            return strResultado;
        }
    }
}
