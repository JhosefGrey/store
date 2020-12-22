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

namespace Store.API.Controllers
{
    public class FacturasController : Controller
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;


        [System.Web.Http.HttpGet]
        public string GetAll()
        {
            List<FacturasBO> lClientes = FacturasBO.GetAll(pConnection);
            string json = JsonConvert.SerializeObject(lClientes);
            return json;
        }

        [HttpGet]
        public string GetById(int id)
        {
            FacturasBO lFactura = new FacturasBO(pConnection ,id);
            string json = JsonConvert.SerializeObject(lFactura);
            return json;
        } 

        [HttpPost]
        public string PostFactura(eFactura pFactura) {
            FacturasBL.GuardarFactura(pConnection, pFactura);
            return null;
        }

        [HttpDelete]
        public void DelelteCliente(int id) {
            FacturasBO lFactura = new FacturasBO(pConnection, id);
            lFactura.Delete(pConnection);
               
               }
    }
}
