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
    public class DetellesController : ApiController
    {
        public string pConnection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDatabase"].ConnectionString;



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/detalles")]
        public List<DetallesFacturaBO> GetAll()
        {
            List<DetallesFacturaBO> lDetalles = DetallesFacturaBO.GetAll(pConnection);
            return lDetalles;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/detalles/GetBy")]
        public List<eDetalleFactura> GetById(Guid id_factura)
        {
            try
            {
                if (id_factura != null)
                {
                    List<eDetalleFactura> lDetalles = DetallesFacturaBO.getDetalleByFactura(pConnection, id_factura);
                    return lDetalles;
                }
                else
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
        [System.Web.Http.Route("api/detalles/add")]
        public IHttpActionResult addDetalle([FromBody] eDetalleFactura pDetalle)
        {
            try
            {
                DetallesFacturaBO lDetalles = new DetallesFacturaBO();
                lDetalles.cantidad_productos = pDetalle.cantidad_productos;
                lDetalles.id_productos = pDetalle.id_productos;
                lDetalles.Save(pConnection);
                return Ok();
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }
    }
}
