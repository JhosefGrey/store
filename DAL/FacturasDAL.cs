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
    public class FacturasDAL
    {
        public static void Update(string pConnection, FacturasEntity pFacturas)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {


                using (SqlCommand lCommand = new SqlCommand("UPDATE tb_facturas SET num=@num,serie=@serie,fecha=@fecha,id_cliente=@id_cliente WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pFacturas.id);
                    lCommand.Parameters.AddWithValue("@num", pFacturas.num);
                    lCommand.Parameters.AddWithValue("@serie", pFacturas.serie);
                    lCommand.Parameters.AddWithValue("@fecha", pFacturas.fecha);
                    lCommand.Parameters.AddWithValue("@id_cliente", pFacturas.id_cliente);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void UpdateTransaction(SqlConnection pConnection, SqlTransaction pTrasaction, FacturasEntity pFacturas)
        {
            using (SqlCommand lCommand = new SqlCommand("UPDATE tb_facturas SET num=@num,serie=@serie,fecha=@fecha,id_cliente=@id_cliente WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTrasaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pFacturas.id);
                lCommand.Parameters.AddWithValue("@num", pFacturas.num);
                lCommand.Parameters.AddWithValue("@serie", pFacturas.serie);
                lCommand.Parameters.AddWithValue("@fecha", pFacturas.fecha);
                lCommand.Parameters.AddWithValue("@id_cliente", pFacturas.id_cliente);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Add(string pConnection, FacturasEntity pFacturas)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_facturas (num,serie,fecha,id_cliente) VALUES (@num,@serie,@fecha,@id_cliente)", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@num", pFacturas.num);
                    lCommand.Parameters.AddWithValue("@serie", pFacturas.serie);
                    lCommand.Parameters.AddWithValue("@fecha", pFacturas.fecha);
                    lCommand.Parameters.AddWithValue("@id_cliente", pFacturas.id_cliente);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void AddTransaction(SqlConnection pConnection, SqlTransaction pTransaction, FacturasEntity pFacturas)
        {
            using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_facturas (num,serie,fecha,id_cliente) VALUES (@num,@serie,@fecha,@id_cliente)", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@num", pFacturas.num);
                lCommand.Parameters.AddWithValue("@serie", pFacturas.serie);
                lCommand.Parameters.AddWithValue("@fecha", pFacturas.fecha);
                lCommand.Parameters.AddWithValue("@id_cliente", pFacturas.id_cliente);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(string pConnection, FacturasEntity pFacturas)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("DELETE FROM tb_facturas WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pFacturas.id);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction, FacturasEntity pFacturas)
        {
            using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_facturas WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pFacturas.id);
                lCommand.ExecuteNonQuery();
            }
        }

        public static bool Exists(string pConnection, FacturasEntity pFacturas)
        {
            bool lExists = false;
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("SELECT COUNT(*) FROM tb_facturas WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pFacturas.id);
                    lConnection.Open();
                    object lReturnValue = lCommand.ExecuteScalar();
                    lConnection.Close();
                    if (!object.ReferenceEquals(lReturnValue, DBNull.Value) && lReturnValue != null)
                    {
                        lExists = Convert.ToInt32(lReturnValue) > 0;
                    }
                }
            }

            return lExists;
        }

        public static List<FacturasEntity> GetAll(string pConnection)
        {
            List<FacturasEntity> lReturnList = new List<FacturasEntity>();

            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT * FROM tb_facturas", lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            FacturasEntity lFacturas = new FacturasEntity();
                            lFacturas.id = Convert.ToInt32(lRow["id"]);
                            lFacturas.num = Convert.ToInt32(lRow["num"]);
                            lFacturas.serie = Convert.ToString(lRow["serie"]);
                            lFacturas.fecha = Convert.ToDateTime(lRow["fecha"]);
                            lFacturas.id_cliente = Convert.ToInt32(lRow["id_cliente"]);
                            lReturnList.Add(lFacturas);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static List<FacturasEntity> GetAllFilter(string pConnection, string pFilter)
        {
            List<FacturasEntity> lReturnList = new List<FacturasEntity>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(string.Concat("SELECT * FROM tb_facturas", pFilter), lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            FacturasEntity lFacturas = new FacturasEntity();
                            lFacturas.id = Convert.ToInt32(lRow["id"]);
                            lFacturas.num = Convert.ToInt32(lRow["num"]);
                            lFacturas.serie = Convert.ToString(lRow["serie"]);
                            lFacturas.fecha = Convert.ToDateTime(lRow["fecha"]);
                            lFacturas.id_cliente = Convert.ToInt32(lRow["id_cliente"]);
                            lReturnList.Add(lFacturas);
                        }
                    }
                }
                return lReturnList;
            }
        }

        public static List<FacturasEntity> GetAllQuery(string pConnection, string pQuery)
        {
            List<FacturasEntity> lReturnList = new List<FacturasEntity>();

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
                            FacturasEntity lFacturas = new FacturasEntity();
                            lFacturas.id = Convert.ToInt32(lRow["id"]);
                            lFacturas.num = Convert.ToInt32(lRow["num"]);
                            lFacturas.serie = Convert.ToString(lRow["serie"]);
                            lFacturas.fecha = Convert.ToDateTime(lRow["fecha"]);
                            lFacturas.id_cliente = Convert.ToInt32(lRow["id_cliente"]);
                            lReturnList.Add(lFacturas);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static FacturasEntity GetSingle(string pConnection, FacturasEntity pFacturas)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM tb_facturas WHERE id=@id", lConnection))
                {

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@id", pFacturas.id);
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        DataRow lRow = lDataTable.Rows[0];
                        FacturasEntity lFacturas = new FacturasEntity();
                        lFacturas.id = Convert.ToInt32(lRow["id"]);
                        lFacturas.num = Convert.ToInt32(lRow["num"]);
                        lFacturas.serie = Convert.ToString(lRow["serie"]);
                        lFacturas.fecha = Convert.ToDateTime(lRow["fecha"]);
                        lFacturas.id_cliente = Convert.ToInt32(lRow["id_cliente"]);
                        return lFacturas;
                    }
                }
            }
            return null;
        }


    }
}
