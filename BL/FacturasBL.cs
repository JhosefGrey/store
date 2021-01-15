using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using BO;
using System.Data.SqlClient;

namespace BL
{
    public class FacturasBL
    {

        public static void GuardarFactura(string Conection, eFactura pFactura)
        {
            try
            {
                FacturasBO lFacturas = new FacturasBO();
                lFacturas.id = Guid.NewGuid().ToString();
                lFacturas.num = FacturasBO.GetNum(Conection, pFactura.serie);
                lFacturas.serie = pFactura.serie;
                lFacturas.fecha = DateTime.Now;
                lFacturas.id_cliente = pFactura.id_cliente;

                List<DetallesFacturaBO> detalles = new List<DetallesFacturaBO>();
                List<ProductosBO> productos = new List<ProductosBO>();

                foreach (var oDetalle in pFactura.Detalles)
                {
                    DetallesFacturaBO lDetalle = new DetallesFacturaBO();
                    ProductosBO lProducto = new ProductosBO(Conection, oDetalle.id_productos);
                    if(lProducto.cantidad < oDetalle.cantidad_productos)
                    {
                        throw new Exception("No hay existencia del producto "+ lProducto.nombre);
                    }
                    lProducto.cantidad = lProducto.cantidad - oDetalle.cantidad_productos;
                    productos.Add(lProducto);
                    lDetalle.total = oDetalle.cantidad_productos * lProducto.precio;
                    lDetalle.cantidad_productos = oDetalle.cantidad_productos;
                    lDetalle.id_productos = oDetalle.id_productos;
                    lDetalle.id_factura = Guid.Parse((string)lFacturas.id);
                    detalles.Add(lDetalle);
                
                }
                SqlConnection lConnection = new SqlConnection(Conection);
                SqlTransaction lTransaction;

                lConnection.Open();
                lTransaction = lConnection.BeginTransaction();

                try
                {
                    lFacturas.SaveTransaction(lConnection, lTransaction);
                    foreach (var iDetalle in detalles)
                    {
                        iDetalle.SaveTransaction(lConnection, lTransaction);
                    }
                    foreach (var iProductos in productos)
                    {
                        iProductos.SaveTransaction(lConnection, lTransaction);
                    }

                    lTransaction.Commit();
                }
                catch
                {
                    lTransaction.Rollback();
                    throw;
                }
                finally
                {
                    lConnection.Close();
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
