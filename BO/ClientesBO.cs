using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL;
using Entities;

namespace BO
{
    public class ClientesBO
    {
        #region Propiedades
        public int id { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string nit { get; set; }
        public Boolean IsNew { get; set; }
        #endregion
        public ClientesBO(string pConnection, int pId)
        {
            ClientesEntity pClientes = new ClientesEntity();
            pClientes.id = pId;

            try
            {
                this.FillEntity(ClientesDAL.GetSingle(pConnection, pClientes));
            }
            catch
            {
                this.FillEntity();
            }
        }

        public ClientesBO()
        {
            this.FillEntity();
        }

        public ClientesBO(ClientesEntity pCLientes)
        {
            try
            {
                this.FillEntity(pCLientes);
            }
            catch
            {
                this.FillEntity();
            }
        }
        #region Publicos

        public void Save(string pConnection)
        {
            ClientesEntity pCLientes = new ClientesEntity();
            pCLientes.id = id;
            pCLientes.apellido = apellido;
            pCLientes.nombre = nombre;
            pCLientes.nit = nit;
            if (IsNew == false)
                ClientesDAL.Update(pConnection, pCLientes);
            else
                ClientesDAL.Add(pConnection, pCLientes);
        }

        public void SaveTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            ClientesEntity pClientes = new ClientesEntity();
            pClientes.id = id;
            pClientes.apellido = apellido;
            pClientes.nombre = nombre;
            pClientes.nit = nit;

            if (IsNew == false)
                ClientesDAL.UpdateTransaction(pConnection, pTransaction, pClientes);
            else
                ClientesDAL.AddTransaction(pConnection, pTransaction, pClientes);
        }

        public void Delete(string pConnection)
        {
            ClientesEntity pClientes = new ClientesEntity();
            pClientes.id = id;
            ClientesDAL.Delete(pConnection, pClientes);
        }

        public void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            ClientesEntity pClientes = new ClientesEntity();
            pClientes.id = id;

            ClientesDAL.DeleteTransaction(pConnection, pTransaction, pClientes);
        }

        public static bool Exists(string pConnection,int pId)
        {
            ClientesEntity pClientes = new ClientesEntity();
            pClientes.id = pId;

            return ClientesDAL.Exists(pConnection, pClientes);
        }

        public static List<ClientesBO> GetAll(string pConnection)
        {
            List<ClientesEntity> lClientes = ClientesDAL.GetAll(pConnection);
            List<ClientesBO> lista = new List<ClientesBO>();

            foreach (ClientesEntity ent in lClientes)
            {
                lista.Add(new ClientesBO(ent));
            }
            return lista;
        }

        public static List<ClientesBO> GetAllFiltro(string pConnection, string pFiltro)
        {
            List<ClientesEntity> lClientes = ClientesDAL.GetAllFilter(pConnection, pFiltro);
            List<ClientesBO> lista = new List<ClientesBO>();

            foreach (ClientesEntity ent in lClientes)
            {
                lista.Add(new ClientesBO(ent));
            }

            return lista;

        } 

        public static List<ClientesBO> GetAllQuery(string pConnection, string pQuery)
        {
            List<ClientesEntity> lClientes = ClientesDAL.GetAllQuery(pConnection, pQuery);
            List<ClientesBO> lista = new List<ClientesBO>();

            foreach (ClientesEntity ent in lClientes)
            {
                lista.Add(new ClientesBO(ent));
            }

            return lista;

        }

        public static ClientesBO GetSingle(string pConnection, ClientesEntity pClientes)
        {
            pClientes = ClientesDAL.GetSingle(pConnection, pClientes);
            return new ClientesBO(pClientes);
        }

        #endregion
        #region Privados

        private void FillEntity(ClientesEntity pClientes)
        {
            try
            {
                id = pClientes.id;
                apellido = pClientes.apellido;
                nombre = pClientes.nombre;
                nit = pClientes.nit;
                IsNew = false;
            }
            catch
            {
                this.FillEntity();
            }
        }


        private void FillEntity()
        {
            id = 0;
            apellido = null;
            nombre = null;
            nit = null;
            IsNew = true;
        }

        #endregion
    }
}
