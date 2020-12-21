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
    public class ProductosBO
    {
        #region Propiedades
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string code { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public Boolean IsNew {get; set;}

        #endregion

        public ProductosBO(string pConnection, int pId)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = pId;

            try
            {
                this.FillEntity(ProductosDAL.GetSingle(pConnection, pProductos));
            }
            catch
            {
                this.FillEntity();
            }
        }

        public ProductosBO()
        {
            this.FillEntity();
        }

        public ProductosBO(ProductosEntity pProductos)
        {
            try
            {
                this.FillEntity(pProductos);
            }
            catch
            {
                this.FillEntity();
            }
        }

        #region Publicos

        public void Save(string pConnection)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = id;
            pProductos.nombre = nombre;
            pProductos.descripcion = descripcion;
            pProductos.code = code;
            pProductos.cantidad = cantidad;
            pProductos.precio = precio;
            if (IsNew == false)
                ProductosDAL.Update(pConnection, pProductos);
            else
                ProductosDAL.Add(pConnection, pProductos);
        }


        public void SaveTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = id;
            pProductos.nombre = nombre;
            pProductos.descripcion = descripcion;
            pProductos.code = code;
            pProductos.cantidad = cantidad;
            pProductos.precio = precio;
            if (IsNew == false)
                ProductosDAL.UpdateTransaction(pConnection, pTransaction, pProductos);
            else
                ProductosDAL.AddTransaction(pConnection, pTransaction, pProductos);
        }

        public void Delete(string pConnection)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = id;

            ProductosDAL.Delete(pConnection, pProductos);
        }

        public void DeleteTransaction(SqlConnection pConnection, SqlTransaction pTransaction)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = id;

            ProductosDAL.DeleteTransaction(pConnection, pTransaction, pProductos);
        }

        public static bool Exists(string pConnection, int id)
        {
            ProductosEntity pProductos = new ProductosEntity();
            pProductos.id = id;

            return ProductosDAL.Exists(pConnection, pProductos);
        }


        public static List<ProductosBO> GetAll(string pConnection)
        {
            List<ProductosEntity> lProductos = ProductosDAL.GetAll(pConnection);
            List<ProductosBO> lista = new List<ProductosBO>();

            foreach (ProductosEntity ent in lProductos)
            {
                lista.Add(new ProductosBO(ent));
            }

            return lista;

        }


        public static List<ProductosBO> GetAllFiltro(string pConnection, string pFiltro)
        {
            List<ProductosEntity> lProductos = ProductosDAL.GetAllFilter(pConnection, pFiltro);
            List<ProductosBO> lista = new List<ProductosBO>();

            foreach (ProductosEntity ent in lProductos)
            {
                lista.Add(new ProductosBO(ent));
            }

            return lista;

        }


        public static List<ProductosBO> GetAllQuery(string pConnection, string pQuery)
        {
            List<ProductosEntity> lProductos = ProductosDAL.GetAllQuery(pConnection, pQuery);
            List<ProductosBO> lista = new List<ProductosBO>();

            foreach (ProductosEntity ent in lProductos)
            {
                lista.Add(new ProductosBO(ent));
            }

            return lista;

        }


        public static ProductosBO GetSingle(string pConnection, ProductosEntity pProductos)
        {
            pProductos = ProductosDAL.GetSingle(pConnection, pProductos);
            return new ProductosBO(pProductos);
        }

        #endregion

        #region Privados

        private void FillEntity(ProductosEntity pProductos)
        {
            try
            {
                id = pProductos.id;
                nombre = pProductos.nombre;
                descripcion = pProductos.descripcion;
                code = pProductos.code;
                cantidad = pProductos.cantidad;
                precio = pProductos.precio;
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
            nombre = null;
            descripcion = null;
            code = null;
            cantidad = 0;
            precio = 0;
            IsNew = true;
        }

        #endregion

    }
}
