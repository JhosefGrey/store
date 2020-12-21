using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using Newtonsoft.Json;

namespace Store.API.Controllers
{
    public class DetellesController : Controller
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;


        [System.Web.Http.HttpGet]
        public string GetAll()
        {
            List<DetallesFacturaBO> lDetalles = DetallesFacturaBO.GetAll(pConnection);
            string json = JsonConvert.SerializeObject(lDetalles);
            return json;
        }
    }
}
