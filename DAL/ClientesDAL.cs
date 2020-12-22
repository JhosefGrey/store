using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class ClientesDAL
    {

        public static void Update(string pConnection, ClientesEntity pClientes)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("UPDATE tb_clientes SET apellido=@apellido,nombre=@nombre,nit=@nit WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pClientes.id);
                    lCommand.Parameters.AddWithValue("@apellido", pClientes.apellido);
                    lCommand.Parameters.AddWithValue("@nombre", pClientes.nombre);
                    if (pClientes.nit == "") lCommand.Parameters.AddWithValue("@nit", "C/F"); else lCommand.Parameters.AddWithValue("@nit", pClientes.nit);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();

                }
            }
        }

        public static void UpdateTransaction(SqlConnection pConnection, SqlTransaction pTrasaction, ClientesEntity pClientes)
        {
            using (SqlCommand lCommand = new SqlCommand("UPDATE tb_clientes SET apellido=@apellido,nombre=@nombre,nit=@nit WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTrasaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pClientes.id);
                lCommand.Parameters.AddWithValue("@apellido", pClientes.apellido);
                lCommand.Parameters.AddWithValue("@nombre", pClientes.nombre);
                if (pClientes.nit == "") lCommand.Parameters.AddWithValue("@nit", DBNull.Value); else lCommand.Parameters.AddWithValue("@nit", pClientes.nit);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Add(string pConnection, ClientesEntity pClientes)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_clientes (apellido,nombre,nit) VALUES (@apellido,@nombre,@nit)", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@apellido", pClientes.apellido);
                    lCommand.Parameters.AddWithValue("@nombre", pClientes.nombre);
                    if (pClientes.nit == "" || pClientes.nit == null) lCommand.Parameters.AddWithValue("@nit", DBNull.Value); else lCommand.Parameters.AddWithValue("@nit", pClientes.nit);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void AddTransaction(SqlConnection pConnection, SqlTransaction pTransaction, ClientesEntity pClientes)
        {
            using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_clientes (apellido,nombre,nit) VALUES (@apellido,@nombre,@nit)", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@apellido", pClientes.apellido);
                lCommand.Parameters.AddWithValue("@nombre", pClientes.nombre);
                if (pClientes.nit == "" || pClientes.nit == null) lCommand.Parameters.AddWithValue("@nit", DBNull.Value); else lCommand.Parameters.AddWithValue("@nit", pClientes.nit);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(string pConnection, ClientesEntity pClientes)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("DELETE FROM tb_clientes WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pClientes.id);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction, ClientesEntity pClientes)
        {
            using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_clientes WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pClientes.id);
                lCommand.ExecuteNonQuery();
            }
        }

        public static bool Exists(string pConnection, ClientesEntity pClientes)
        {
            bool lExists = false;
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("SELECT COUNT(*) FROM tb_clientes WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pClientes.id);
                    lConnection.Open();
                    object lReturnValue = lCommand.ExecuteScalar();
                    lConnection.Close();
                    if(!object.ReferenceEquals(lReturnValue,DBNull.Value) && lReturnValue != null)
                    {
                        lExists = Convert.ToInt32(lReturnValue) > 0;
                    }
                }
            }

            return lExists;
        }

        public static List<ClientesEntity> GetAll(string pConnection)
        {
            List<ClientesEntity> lReturnList = new List<ClientesEntity>();

            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT * FROM tb_clientes", lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if(lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            ClientesEntity lClientes = new ClientesEntity();
                            lClientes.id = Convert.ToInt32(lRow["id"]);
                            lClientes.apellido = Convert.ToString(lRow["apellido"]);
                            lClientes.nombre = Convert.ToString(lRow["nombre"]);
                            if(!object.ReferenceEquals(lRow["nit"], DBNull.Value) && lRow["nit"] != null)
                            {
                                lClientes.nit = Convert.ToString(lRow["nit"]);
                            }
                            lReturnList.Add(lClientes);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static List<ClientesEntity> GetAllFilter(string pConnection, string pFilter)
        {
            List<ClientesEntity> lReturnList = new List<ClientesEntity>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(string.Concat("SELECT * FROM tb_clientes", pFilter), lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            ClientesEntity lClientes = new ClientesEntity();
                            lClientes.id = Convert.ToInt32(lRow["id"]);
                            lClientes.apellido = Convert.ToString(lRow["apellido"]);
                            lClientes.nombre = Convert.ToString(lRow["nombre"]);
                            if (!object.ReferenceEquals(lRow["nit"], DBNull.Value) && lRow["nit"] != null)
                            {
                                lClientes.nit = Convert.ToString(lRow["nit"]);
                            }
                            lReturnList.Add(lClientes);
                        }
                    }
                }
                return lReturnList;
            }
        }

        public static List<ClientesEntity> GetAllQuery(string pConnection, string pQuery)
        {
            List<ClientesEntity> lReturnList = new List<ClientesEntity>();

            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(pQuery, lConnection))
                {

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            ClientesEntity lClientes = new ClientesEntity();
                            lClientes.id = Convert.ToInt32(lRow["id"]);
                            lClientes.apellido = Convert.ToString(lRow["nombre"]);
                            lClientes.nombre = Convert.ToString(lRow["apellido"]);
                            if (!object.ReferenceEquals(lRow["nit"], DBNull.Value) && lRow["nit"] != null)
                            {
                                lClientes.nit = Convert.ToString(lRow["nit"]);
                            }
                            lReturnList.Add(lClientes);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static ClientesEntity GetSingle(string pConnection, ClientesEntity pClientes)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM tb_clientes WHERE id=@id", lConnection))
                {

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@id", pClientes.id);
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        DataRow lRow = lDataTable.Rows[0];
                        ClientesEntity lCLientes = new ClientesEntity();
                        lCLientes.id = Convert.ToInt32(lRow["id"]);
                        lCLientes.apellido = Convert.ToString(lRow["apellido"]);
                        lCLientes.nombre = Convert.ToString(lRow["nombre"]);
                        if (!object.ReferenceEquals(lRow["nit"], DBNull.Value) && lRow["nit"] != null)
                        {
                            lCLientes.nit = Convert.ToString(lRow["nit"]);
                        }
                        return lCLientes;
                    }
                }
            }
            return null;
        }

    }
}
