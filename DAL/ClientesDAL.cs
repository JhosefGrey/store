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
    class ClientesDAL
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
                    if (pClientes.nit == "") lCommand.Parameters.AddWithValue("@nit", DBNull.Value); else lCommand.Parameters.AddWithValue("@nit", pClientes.nit);
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
                using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_clientes WHERE id=@id", lConnection))
                {
                    lCommand.CommandType = CommandType.Text;
                    lCommand.Parameters.AddWithValue("@id", pClientes.id);
                    lConnection.Open();
                    lCommand.ExecuteNonQuery();
                    lConnection.Close();
                }
            }
        }


        public static void DeleteTransaction(SqlConnection pConnection,SqlTransaction pTransaction, ClientesEntity pClientes)
        {

            using (SqlCommand lCommand = new SqlCommand("DELETE FFROM tb_clientes WHERE id=@id", pConnection))
            {
                lCommand.Transaction = pTransaction;
                lCommand.CommandType = CommandType.Text;
                lCommand.Parameters.AddWithValue("@id", pClientes.id);
                lCommand.ExecuteNonQuery();
            }
            
        }

    }
}
