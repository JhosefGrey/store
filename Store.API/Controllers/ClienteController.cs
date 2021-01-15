using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BO;
using Newtonsoft.Json;
using Entidades;

namespace Store.API.Controllers
{
    public class ClienteController : ApiController
    {

        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Cliente/GetAll")]
        public List<ClientesBO> GetAll()
        {
            List<ClientesBO> lClientes = ClientesBO.GetAll(pConnection);
            return lClientes;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Cliente/GetById")]
        public ClientesBO GetById(int id)
        {
            ClientesBO lCliente = new ClientesBO(pConnection, id);
            return lCliente;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Cliente/GetByFilter")]
        public List<ClientesBO> GetAllFiltro([FromBody] eCliente pCliente)
        {
            try
            {
                if (pCliente.nombre == null && pCliente.nit != "")
                {
                    var filtro = " WHERE nit LIKE " + "'" + "%" + pCliente.nit + "%" + "'";
                    List<ClientesBO> lCliente = ClientesBO.GetAllFiltro(pConnection, filtro);
                    return lCliente;
                }
                if (pCliente.nit == null && pCliente.nombre != "")
                {
                    var filtro = " WHERE nombre LIKE " + "'" + "%" + pCliente.nombre + "%" + "'";
                    List<ClientesBO> lCliente = ClientesBO.GetAllFiltro(pConnection, filtro);
                    return lCliente;
                }else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Cliente/AddCliente")]
        public IHttpActionResult AddCliente([FromBody] eCliente pCliente)
        {
            try
            {
                ClientesBO lClientes = new ClientesBO();
                lClientes.apellido = pCliente.apellido;
                lClientes.nombre = pCliente.nombre;
                lClientes.nit = pCliente.nit;
                lClientes.Save(pConnection);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Cliente/UpdateCliente")]
        public IHttpActionResult UpdateCliente([FromBody] eCliente pCliente)
        {
            try
            {
                ClientesBO lClientes = new ClientesBO(pConnection, pCliente.id);
                lClientes.id = pCliente.id;
                lClientes.apellido = pCliente.apellido;
                lClientes.nombre = pCliente.nombre;
                lClientes.nit = pCliente.nit;
                lClientes.IsNew = false;
                lClientes.Save(pConnection);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Cliente/DeleteCliente")]
        public IHttpActionResult DeleteCliente(int id)
        {
            try
            {
                ClientesBO cliente = new ClientesBO(pConnection, id);
                cliente.Delete(pConnection);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }


    }


}