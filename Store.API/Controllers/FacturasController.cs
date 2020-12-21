using BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
