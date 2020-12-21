﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BO;
using Newtonsoft.Json;

namespace Store.API.Controllers
{
    public class ClienteController: Controller
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;
        
        [System.Web.Http.HttpGet]
        public string GetAll()
        {
            List<ClientesBO> lClientes = ClientesBO.GetAll(pConnection);
            string json = JsonConvert.SerializeObject(lClientes);
            return json;
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult GetSingle(int id)
        {
            List<ClientesBO> lCliente = ClientesBO.GetSingle(pConnection, id);
        } 
    }
}