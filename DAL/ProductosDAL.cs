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
    public class ProductosDAL
    {
        public static void Update(string pConnection, ProductosEntity pProductos)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("UPDATE tb_productos SET nombre=@nombre,descripcion=@descripcion,code=@code,cantidad=@cantidad,precio=@precio WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pProductos.id);
                    lCommand.Parameters.AddWithValue("@nombre", pProductos.nombre);
                    lCommand.Parameters.AddWithValue("@descripcion", pProductos.descripcion);
                    lCommand.Parameters.AddWithValue("@code", pProductos.code);
                    lCommand.Parameters.AddWithValue("@cantidad", pProductos.cantidad);
                    lCommand.Parameters.AddWithValue("@precio", pProductos.precio);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void UpdateTransaction(SqlConnection pConnection, SqlTransaction pTrasaction, ProductosEntity pProductos)
        {
            using (SqlCommand lCommand = new SqlCommand("UPDATE tb_productos SET nombre=@nombre,descripcion=@descripcion,code=@code,cantidad=@cantidad,precio=@precio WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTrasaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pProductos.id);
                lCommand.Parameters.AddWithValue("@nombre", pProductos.nombre);
                lCommand.Parameters.AddWithValue("@descripcion", pProductos.descripcion);
                lCommand.Parameters.AddWithValue("@code", pProductos.code);
                lCommand.Parameters.AddWithValue("@cantidad", pProductos.cantidad);
                lCommand.Parameters.AddWithValue("@precio", pProductos.precio);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Add(string pConnection,  ProductosEntity pProductos)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_productos (nombre,descripcion,code,cantidad,precio) VALUES (@nombre,@descripcion,@code,@cantidad,@precio)", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pProductos.id);
                    lCommand.Parameters.AddWithValue("@nombre", pProductos.nombre);
                    lCommand.Parameters.AddWithValue("@descripcion", pProductos.descripcion);
                    lCommand.Parameters.AddWithValue("@code", pProductos.code);
                    lCommand.Parameters.AddWithValue("@cantidad", pProductos.cantidad);
                    lCommand.Parameters.AddWithValue("@precio", pProductos.precio);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void AddTransaction(SqlConnection pConnection, SqlTransaction pTransaction, ProductosEntity pProductos)
        {
            using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_productos (nombre,descripcion,code,cantidad,precio) VALUES (@nombre,@descripcion,@code,@cantidad,@precio)", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pProductos.id);
                lCommand.Parameters.AddWithValue("@nombre", pProductos.nombre);
                lCommand.Parameters.AddWithValue("@descripcion", pProductos.descripcion);
                lCommand.Parameters.AddWithValue("@code", pProductos.code);
                lCommand.Parameters.AddWithValue("@cantidad", pProductos.cantidad);
                lCommand.Parameters.AddWithValue("@precio", pProductos.precio);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(string pConnection, ProductosEntity pProductos)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("DELETE FROM tb_productos WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pProductos.id);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction, ProductosEntity pProductos)
        {
            using (SqlCommand lCommand = new SqlCommand("DELETE FROM tb_productos WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pProductos.id);
                lCommand.ExecuteNonQuery();
            }
        }

        public static bool Exists(string pConnection, ProductosEntity pProductos)
        {
            bool lExists = false;
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("SELECT COUNT(*) FROM tb_productos WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pProductos.id);
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

        public static List<ProductosEntity> GetAll(string pConnection)
        {
            List<ProductosEntity> lReturnList = new List<ProductosEntity>();

            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT * FROM tb_productos", lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            ProductosEntity lProductos = new ProductosEntity();
                            lProductos.id = Convert.ToInt32(lRow["id"]);
                            lProductos.nombre = Convert.ToString(lRow["nombre"]);
                            lProductos.descripcion = Convert.ToString(lRow["descripcion"]);
                            lProductos.code = Convert.ToString(lRow["code"]);
                            lProductos.cantidad = Convert.ToInt32(lRow["cantidad"]);
                            lProductos.precio = Convert.ToDecimal(lRow["precio"]);
                            lReturnList.Add(lProductos);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static List<ProductosEntity> GetAllFilter(string pConnection, string pFilter)
        {
            List<ProductosEntity> lReturnList = new List<ProductosEntity>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(string.Concat("SELECT * FROM tb_productos", pFilter), lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            ProductosEntity lProductos = new ProductosEntity();
                            lProductos.id = Convert.ToInt32(lRow["id"]);
                            lProductos.nombre = Convert.ToString(lRow["nombre"]);
                            lProductos.descripcion = Convert.ToString(lRow["descripcion"]);
                            lProductos.code = Convert.ToString(lRow["code"]);
                            lProductos.cantidad = Convert.ToInt32(lRow["cantidad"]);
                            lProductos.precio = Convert.ToDecimal(lRow["precio"]);
                            lReturnList.Add(lProductos);
                        }
                    }
                }
                return lReturnList;
            }
        }

        public static List<ProductosEntity> GetAllQuery(string pConnection, string pQuery)
        {
            List<ProductosEntity> lReturnList = new List<ProductosEntity>();

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
                            ProductosEntity lProductos = new ProductosEntity();
                            lProductos.id = Convert.ToInt32(lRow["id"]);
                            lProductos.nombre = Convert.ToString(lRow["nombre"]);
                            lProductos.descripcion = Convert.ToString(lRow["descripcion"]);
                            lProductos.code = Convert.ToString(lRow["code"]);
                            lProductos.cantidad = Convert.ToInt32(lRow["cantidad"]);
                            lProductos.precio = Convert.ToDecimal(lRow["precio"]);
                            lReturnList.Add(lProductos);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static ProductosEntity GetSingle(string pConnection, ProductosEntity pProductos)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM tb_productos WHERE id=@id", lConnection))
                {

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@id", pProductos.id);
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        DataRow lRow = lDataTable.Rows[0];
                        ProductosEntity lProductos = new ProductosEntity();
                        lProductos.id = Convert.ToInt32(lRow["id"]);
                        lProductos.nombre = Convert.ToString(lRow["nombre"]);
                        lProductos.descripcion = Convert.ToString(lRow["descripcion"]);
                        lProductos.code = Convert.ToString(lRow["code"]);
                        lProductos.cantidad = Convert.ToInt32(lRow["cantidad"]);
                        lProductos.precio = Convert.ToDecimal(lRow["precio"]);
                        return lProductos;
                    }
                }
            }
            return null;
        }
    }
}
