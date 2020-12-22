using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using Newtonsoft.Json;
using Entidades;
using System.Threading.Tasks;
using System.Web.Http;

namespace Store.API.Controllers
{
    public class ProductosController : ApiController
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Producto/GetAll")]
        public string GetAll()
        {
            List<ProductosBO> lProductos = ProductosBO.GetAll(pConnection);
            string json = JsonConvert.SerializeObject(lProductos);
            return json;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Producto/GetById")]
        public string GetById(int id)
        {
            try
            {
                ProductosBO lProdcuto = new ProductosBO(pConnection, id);
                string json = JsonConvert.SerializeObject(lProdcuto);
                return json;
            }
            catch (Exception e)
            {
                return InternalServerError(e).ToString();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Producto/addProducto")]
        public IHttpActionResult addProducto([FromBody]eProducto pProducto)
        {
            try
            {
                ProductosBO lProducto = new ProductosBO();
                lProducto.nombre = pProducto.nombre;
                lProducto.descripcion = pProducto.descripcion;
                lProducto.code = pProducto.code;
                lProducto.cantidad = pProducto.cantidad;
                lProducto.precio = pProducto.precio;
                lProducto.Save(pConnection);
                return Ok();

            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Producto/updateProducto")]
        public IHttpActionResult UpdateProducto([FromBody] eProducto pProducto)
        {
            try
            {
                ProductosBO lProducto = new ProductosBO(pConnection, pProducto.id);
                lProducto.nombre = pProducto.nombre;
                lProducto.descripcion = pProducto.descripcion;
                lProducto.code = pProducto.code;
                lProducto.cantidad = pProducto.cantidad;
                lProducto.precio = pProducto.precio;
                lProducto.Save(pConnection);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Producto/deleteProducto")]
        public IHttpActionResult DeleteProducto(int id)
        {
            try
            {
                ProductosBO prodcuto = new ProductosBO(pConnection, id);
                prodcuto.Delete(pConnection);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }


    }
}
