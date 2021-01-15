using BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using BL;
using System.Web.Http;

namespace Store.API.Controllers
{
    public class FacturasController : ApiController
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/facturas")]
        public List<eFactura> GetAll()
        {
            List<eFactura> lClientes = FacturasBO.getClienteById(pConnection);
            return lClientes;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/factura/GetById")]
        public FacturasBO GetById(string id)
        {
            FacturasBO lFactura = new FacturasBO(pConnection ,id);
            return lFactura;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/factura/getNum")]
        public FacturasBO GetNum(string serie)
        {
            FacturasBO lFactura = new FacturasBO(pConnection, serie);
            return lFactura;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/factura/addFactura")]
        public IHttpActionResult PostFactura(eFactura pFactura) {
            FacturasBL.GuardarFactura(pConnection, pFactura);
            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public void DelelteCliente(string id) {
            FacturasBO lFactura = new FacturasBO(pConnection, id);
            lFactura.Delete(pConnection);
               
               }
    }
}
