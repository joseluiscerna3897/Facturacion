using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Entity.BusinessEntity;

namespace Facturacion.Entity.DataAccess
{
    public class Conexion
    {
        SqlConnection cn = new SqlConnection("Data Source=JOSE\\SQLEXPRESS;Initial Catalog=FacturacionBD;Persist Security Info=True;User ID=sa;Password=123");

        public SqlConnection NewConnection
        {
            get { return cn; }
        }

        public void pAddParameter(SqlCommand command, String parameterName, Object value, DbType dbTipo)
        {
            pAddParameter(command, parameterName, value, dbTipo, ParameterDirection.Input);
        }

        public void pAddParameter(SqlCommand command, String parameterName, Object value, SqlDbType dbTipo)
        {
            pAddParameter(command, parameterName, value, dbTipo, ParameterDirection.Input);
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, DbType dbTipo, ParameterDirection parameterDirection)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.DbType = dbTipo;
            parameter.Direction = parameterDirection;
            command.Parameters.Add(parameter);
        }

        protected void pAddParameter(SqlCommand command, String parameterName, Object value, SqlDbType dbTipo, ParameterDirection parameterDirection)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.SqlDbType = dbTipo;
            parameter.Direction = parameterDirection;
            command.Parameters.Add(parameter);
        }


        protected String fEjecutar(SqlCommand cmd)
        {
            String Resultado = "";
            try
            {
                NewConnection.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Resultado = "OK";
                }
                else
                {
                    Resultado = "La información se registró anteriormente.";
                }
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                Resultado = ex.Message;
            }
            finally
            {
                if (NewConnection.State != ConnectionState.Closed)
                {
                    NewConnection.Close();
                }
            }
            return Resultado;
        }

        public SqlDataReader fLeer(SqlCommand cmd)
        {
            try
            {
                NewConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                if (NewConnection.State != ConnectionState.Closed)
                {
                    NewConnection.Close();
                }
                throw new Exception("Método Leer: " + ex.Message, ex);
            }
        }

        public DataTable fSeleccionar(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                DataTable dt = new DataTable();
                DbDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Seleccionar: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        protected DataSet fDSEjecutar(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                DataSet dt = new DataSet();
                DbDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Seleccionar: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        protected Object fObtenerValor(IDbCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Obtener Valor: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }
        protected Int32 fEjecutarQuery(IDbCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();
                }
                throw new Exception("Método Obtener Valor: " + ex.Message, ex);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

        public Object ConvertirDataReaderALista<T>(SqlDataReader myDataReader)
        {
            List<T> entidades = new List<T>();
            PropertyInfo[] propiedades = typeof(T).GetProperties();
            try
            {
                ArrayList columnasConsultadas = new ArrayList();
                for (int i = 0; i < myDataReader.FieldCount; i++) { columnasConsultadas.Add(myDataReader.GetName(i)); }
                while (myDataReader.Read())
                {
                    T entidad = Activator.CreateInstance<T>();
                    foreach (PropertyInfo propiedad in propiedades)
                    {
                        Attribute item = Attribute.GetCustomAttribute(propiedad, typeof(BEColumn));
                        if (item is BEColumn)
                        {
                            try
                            {
                                BEColumn column = (BEColumn)item;
                                if (columnasConsultadas.IndexOf(column.Name) > -1)
                                {
                                    propiedad.SetValue(entidad, myDataReader.GetValue(myDataReader.GetOrdinal(column.Name)), null);
                                }
                            }
                            catch (Exception ex) { throw new Exception(ex.Message); }
                        }
                    }
                    entidades.Add(entidad);
                }
            }
            finally
            {
                myDataReader.Close();
            }
            return entidades;
        }
    }
}
