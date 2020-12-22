using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;
using System.Data.SqlClient;

namespace BO
{
    public class FacturasBO
    {

        #region Properties 
        public int id { get; set; }
        public int num { get; set; }
        public string serie { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;
        public int id_cliente { get; set; }
        public bool IsNew { get; set; }
        #endregion

        public FacturasBO(string pConnection, int pId)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = pId;

            try
            {
                this.FillEntity(FacturasDAL.GetSingle(pConnection, pFacturas));
            }
            catch(Exception e)
            {
                this.FillEntity();
          
            }
        }

        public FacturasBO()
        {
            this.FillEntity();
        }

        public FacturasBO(FacturasEntity pFacturas)
        {
            try
            {
                this.FillEntity(pFacturas);
            }
            catch
            {
                this.FillEntity();
            }
        }

        #region Publicos

        public void Save(string pConnection)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = id;
            pFacturas.num = num;
            pFacturas.fecha = fecha;
            pFacturas.serie = serie;
            pFacturas.id_cliente = id_cliente;
            if (IsNew == false)
                FacturasDAL.Update(pConnection, pFacturas);
            else
                FacturasDAL.Add(pConnection, pFacturas);
        }

        public void SaveTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = id;
            pFacturas.num = num;
            pFacturas.serie = serie;
            pFacturas.id_cliente = id_cliente;

            if (IsNew == false)
                FacturasDAL.UpdateTransaction(pConnection, pTransaction, pFacturas);
            else
                FacturasDAL.AddTransaction(pConnection, pTransaction, pFacturas);
        }

        public void Delete(string pConnection)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = id;
            FacturasDAL.Delete(pConnection, pFacturas);
        }

        public void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = id;

            FacturasDAL.DeleteTransaction(pConnection, pTransaction, pFacturas);
        }

        public static bool Exists(string pConnection, int pId)
        {
            FacturasEntity pFacturas = new FacturasEntity();
            pFacturas.id = pId;

            return FacturasDAL.Exists(pConnection, pFacturas);
        }

        public static List<FacturasBO> GetAll(string pConnection)
        {
            List<FacturasEntity> lFacturas = FacturasDAL.GetAll(pConnection);
            List<FacturasBO> lista = new List<FacturasBO>();

            foreach (FacturasEntity ent in lFacturas)
            {
                lista.Add(new FacturasBO(ent));
            }
            return lista;
        }

        public static List<FacturasBO> GetAllFiltro(string pConnection, string pFiltro)
        {
            List<FacturasEntity> lFacturas = FacturasDAL.GetAllFilter(pConnection, pFiltro);
            List<FacturasBO> lista = new List<FacturasBO>();

            foreach (FacturasEntity ent in lFacturas)
            {
                lista.Add(new FacturasBO(ent));
            }

            return lista;

        }


        public static List<FacturasBO> GetAllQuery(string pConnection, string pQuery)
        {
            List<FacturasEntity> lFacturas = FacturasDAL.GetAllQuery(pConnection, pQuery);
            List<FacturasBO> lista = new List<FacturasBO>();

            foreach (FacturasEntity ent in lFacturas)
            {
                lista.Add(new FacturasBO(ent));
            }

            return lista;

        }


        public static FacturasBO GetSingle(string pConnection, FacturasEntity pFacturas)
        {
            pFacturas = FacturasDAL.GetSingle(pConnection, pFacturas);
            return new FacturasBO(pFacturas);
        }

        #endregion
        #region Privados

        private void FillEntity(FacturasEntity pFacturas)
        {
            try
            {
                id = pFacturas.id;
                num = pFacturas.num;
                serie = pFacturas.serie;
                fecha = pFacturas.fecha;
                id_cliente = pFacturas.id_cliente;
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
            num = 0;
            serie = null;
            fecha = DateTime.Now;
            id_cliente = 0;
            IsNew = true;
        }

        #endregion
    }
}
