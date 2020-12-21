﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    class DetallesFactura
    {
        public static void Update(string pConnection, DetallesFacturaEntity pDetalle)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("UPDATE tb_detalles_factura SET cantidad_productos=@cantidad_productos, total=@total,id_factura=@id_factura,id_producto=@id_producto WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pDetalle.id);
                    lCommand.Parameters.AddWithValue("@cantidad_productos", pDetalle.cantidad_productos);
                    lCommand.Parameters.AddWithValue("@total", pDetalle.total);
                    lCommand.Parameters.AddWithValue("@id_factura", pDetalle.id_factura);
                    lCommand.Parameters.AddWithValue("@id_producto", pDetalle.id_productos);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void UpdateTransaction(SqlConnection pConnection, SqlTransaction pTrasaction, DetallesFacturaEntity pDetalle)
        {
            using (SqlCommand lCommand = new SqlCommand("UPDATE tb_detalles_factura SET cantidad_productos=@cantidad_productos, total=@total,id_factura=@id_factura,id_producto=@id_producto WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTrasaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pDetalle.id);
                lCommand.Parameters.AddWithValue("@cantidad_productos", pDetalle.cantidad_productos);
                lCommand.Parameters.AddWithValue("@total", pDetalle.total);
                lCommand.Parameters.AddWithValue("@id_factura", pDetalle.id_factura);
                lCommand.Parameters.AddWithValue("@id_producto", pDetalle.id_productos);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Add(string pConnection, DetallesFacturaEntity pDetalle)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_detalles_factura (cantiad_productos,total,id_factura,id_producto) VALUES (@cantiad_productos,@total,@id_factura,@id_producto)", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@cantidad_productos", pDetalle.cantidad_productos);
                    lCommand.Parameters.AddWithValue("@total", pDetalle.total);
                    lCommand.Parameters.AddWithValue("@id_factura", pDetalle.id_factura);
                    lCommand.Parameters.AddWithValue("@id_producto", pDetalle.id_productos);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }

        public static void AddTransaction(SqlConnection pConnection, SqlTransaction pTransaction, DetallesFacturaEntity pDetalle)
        {
            using (SqlCommand lCommand = new SqlCommand("INSERT INTO tb_detalles_factura (cantiad_productos,total,id_factura,id_producto) VALUES (@cantiad_productos,@total,@id_factura,@id_producto)", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@cantidad_productos", pDetalle.cantidad_productos);
                lCommand.Parameters.AddWithValue("@total", pDetalle.total);
                lCommand.Parameters.AddWithValue("@id_factura", pDetalle.id_factura);
                lCommand.Parameters.AddWithValue("@id_producto", pDetalle.id_productos);
                lCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(string pConnection, ProductosEntity pProductos)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_detalles_factura WHERE id=@id", lConnection))
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
            using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_detalles_factura WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pProductos.id);
                lCommand.ExecuteNonQuery();
            }
        }

        public static bool Exists(string pConnection, DetallesFacturaEntity pDetalle)
        {
            bool lExists = false;
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlCommand lCommand = new SqlCommand("SELECT COUNT(*) FROM tb_detalles_factura WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pDetalle.id);
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

        public static List<DetallesFacturaEntity> GetAll(string pConnection)
        {
            List<DetallesFacturaEntity> lReturnList = new List<DetallesFacturaEntity>();

            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT * FROM tb_detalles_factura", lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            DetallesFacturaEntity lDetalles = new DetallesFacturaEntity();
                            lDetalles.id = Convert.ToInt32(lRow["id"]);
                            lDetalles.cantidad_productos = Convert.ToInt32(lRow["cantidad_productos"]);
                            lDetalles.total = Convert.ToDecimal(lRow["total"]);
                            lDetalles.id_factura = Convert.ToInt32(lRow["id_factura"]);
                            lDetalles.id_productos = Convert.ToInt32(lRow["id_productos"]);
                            lReturnList.Add(lDetalles);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static List<DetallesFacturaEntity> GetAllFilter(string pConnection, string pFilter)
        {
            List<DetallesFacturaEntity> lReturnList = new List<DetallesFacturaEntity>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(string.Concat("SELECT * FROM tb_detalles_productos", pFilter), lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            DetallesFacturaEntity lDetalles = new DetallesFacturaEntity();
                            lDetalles.id = Convert.ToInt32(lRow["id"]);
                            lDetalles.cantidad_productos = Convert.ToInt32(lRow["cantidad_productos"]);
                            lDetalles.total = Convert.ToDecimal(lRow["total"]);
                            lDetalles.id_factura = Convert.ToInt32(lRow["id_factura"]);
                            lDetalles.id_productos = Convert.ToInt32(lRow["id_productos"]);
                            lReturnList.Add(lDetalles);
                        }
                    }
                }
                return lReturnList;
            }
        }

        public static List<DetallesFacturaEntity> GetAllQuery(string pConnection, string pQuery)
        {
            List<DetallesFacturaEntity> lReturnList = new List<DetallesFacturaEntity>();

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
                            DetallesFacturaEntity lDetalles = new DetallesFacturaEntity();
                            lDetalles.id = Convert.ToInt32(lRow["id"]);
                            lDetalles.cantidad_productos = Convert.ToInt32(lRow["cantidad_productos"]);
                            lDetalles.total = Convert.ToDecimal(lRow["total"]);
                            lDetalles.id_factura = Convert.ToInt32(lRow["id_factura"]);
                            lDetalles.id_productos = Convert.ToInt32(lRow["id_productos"]);
                            lReturnList.Add(lDetalles);
                        }
                    }
                }
            }
            return lReturnList;
        }

        public static DetallesFacturaEntity GetSingle(string pConnection, DetallesFacturaEntity pDetalles)
        {
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM tb_detalles_factura WHERE id=@id", lConnection))
                {

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@id", pDetalles.id);
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        DataRow lRow = lDataTable.Rows[0];
                        DetallesFacturaEntity lDetalles = new DetallesFacturaEntity();
                        lDetalles.id = Convert.ToInt32(lRow["id"]);
                        lDetalles.cantidad_productos = Convert.ToInt32(lRow["cantidad_productos"]);
                        lDetalles.total = Convert.ToDecimal(lRow["total"]);
                        lDetalles.id_factura = Convert.ToInt32(lRow["id_factura"]);
                        lDetalles.id_productos = Convert.ToInt32(lRow["id_productos"]);
                        return lDetalles;
                    }
                }
            }
            return null;
        }
    }
}
