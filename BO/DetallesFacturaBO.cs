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
    public class DetallesFacturaBO
    {
    #region properties
        public int id { get; set; }
        public int cantidad_productos { get; set; }
        public decimal total { get; set; }
        public int id_factura { get; set; }
        public int id_productos { get; set; }
        public bool IsNew { get; set; }
        #endregion

        public DetallesFacturaBO(string pConnection, int pId)
        {
            DetallesFacturaEntity pDetalles = new DetallesFacturaEntity();
            pDetalles.id = pId;

            try
            {
                this.FillEntity(DetallesFacturaDAL.GetSingle(pConnection, pDetalles));
            }
            catch
            {
                this.FillEntity();
            }
        }

        public DetallesFacturaBO()
        {
            this.FillEntity();
        }

        public DetallesFacturaBO(DetallesFacturaEntity pDetalles)
        {
            try
            {
                this.FillEntity(pDetalles);
            }
            catch
            {
                this.FillEntity();
            }
        }

        #region Publicos

        public void Save(string pConnection)
        {
            DetallesFacturaEntity pDetalles = new DetallesFacturaEntity();
            pDetalles.id = id;
            pDetalles.cantidad_productos = cantidad_productos;
            pDetalles.total = total;
            pDetalles.id_factura = id_factura;
            pDetalles.id_productos = id_productos;
            if (IsNew == false)
                DetallesFacturaDAL.Update(pConnection, pDetalles);
            else
                DetallesFacturaDAL.Add(pConnection, pDetalles);
        }

        public void SaveTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            DetallesFacturaEntity pDettalles = new DetallesFacturaEntity();
            pDettalles.id = id;
            pDettalles.cantidad_productos = cantidad_productos;
            pDettalles.total = total;
            pDettalles.id_factura = id_factura;
            pDettalles.id_productos = id_productos ;

            if (IsNew == false)
                DetallesFacturaDAL.UpdateTransaction(pConnection, pTransaction, pDettalles);
            else
                DetallesFacturaDAL.AddTransaction(pConnection, pTransaction, pDettalles);
        }

        public void Delete(string pConnection)
        {
            DetallesFacturaEntity pDetalles = new DetallesFacturaEntity();
            pDetalles.id = id;
            DetallesFacturaDAL.Delete(pConnection, pDetalles);
        }

        public void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            DetallesFacturaEntity pDetalles = new DetallesFacturaEntity();
            pDetalles.id = id;

            DetallesFacturaDAL.DeleteTransaction(pConnection, pTransaction, pDetalles);
        }

        public static bool Exists(string pConnection, int pId)
        {
            DetallesFacturaEntity pDetalles = new DetallesFacturaEntity();
            pDetalles.id = pId;

            return DetallesFacturaDAL.Exists(pConnection, pDetalles);
        }

        public static List<DetallesFacturaBO> GetAll(string pConnection)
        {
            List<DetallesFacturaEntity> lDetalles = DetallesFacturaDAL.GetAll(pConnection);
            List<DetallesFacturaBO> lista = new List<DetallesFacturaBO>();

            foreach (DetallesFacturaEntity ent in lDetalles)
            {
                lista.Add(new DetallesFacturaBO(ent));
            }
            return lista;
        }

        public static List<DetallesFacturaBO> GetAllFiltro(string pConnection, string pFiltro)
        {
            List<DetallesFacturaEntity> lDetalles = DetallesFacturaDAL.GetAllFilter(pConnection, pFiltro);
            List<DetallesFacturaBO> lista = new List<DetallesFacturaBO>();

            foreach (DetallesFacturaEntity ent in lDetalles)
            {
                lista.Add(new DetallesFacturaBO(ent));
            }

            return lista;

        }


        public static List<DetallesFacturaBO> GetAllQuery(string pConnection, string pQuery)
        {
            List<DetallesFacturaEntity> lDetalles = DetallesFacturaDAL.GetAllQuery(pConnection, pQuery);
            List<DetallesFacturaBO> lista = new List<DetallesFacturaBO>();

            foreach (DetallesFacturaEntity ent in lDetalles)
            {
                lista.Add(new DetallesFacturaBO(ent));
            }

            return lista;

        }


        public static DetallesFacturaBO GetSingle(string pConnection, DetallesFacturaEntity pDetalles)
        {
            pDetalles = DetallesFacturaDAL.GetSingle(pConnection, pDetalles);
            return new DetallesFacturaBO(pDetalles);
        }

        #endregion
        #region Privados

        private void FillEntity(DetallesFacturaEntity pDetalle)
        {
            try
            {
                id = pDetalle.id;
                cantidad_productos = pDetalle.cantidad_productos;
                total = pDetalle.total;
                id_factura = pDetalle.id_factura;
                id_productos = pDetalle.id_productos;
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
            cantidad_productos = 0;
            total = 0;
            id_factura = 0;
            id_productos = 0;
            IsNew = true;
        }

        #endregion
    }
}
