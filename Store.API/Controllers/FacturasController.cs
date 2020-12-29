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
        public string GetAll()
        {
            List<FacturasBO> lClientes = FacturasBO.GetAll(pConnection);
            string json = JsonConvert.SerializeObject(lClientes);
            return json;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/factura/GetById")]
        public string GetById(string id)
        {
            FacturasBO lFactura = new FacturasBO(pConnection ,id);
            string json = JsonConvert.SerializeObject(lFactura);
            return json;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/factura/getNum")]
        public string GetNum(string serie)
        {
            FacturasBO lFactura = new FacturasBO(pConnection, serie);
            string json = JsonConvert.SerializeObject(lFactura);
            return json;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/factura/addFactura")]
        public string PostFactura(eFactura pFactura) {
            FacturasBL.GuardarFactura(pConnection, pFactura);
            return null;
        }

        [System.Web.Http.HttpDelete]
        public void DelelteCliente(string id) {
            FacturasBO lFactura = new FacturasBO(pConnection, id);
            lFactura.Delete(pConnection);
               
               }
    }
}
