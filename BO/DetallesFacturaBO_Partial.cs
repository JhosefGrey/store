using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Entities;

namespace BO
{
    public partial class DetallesFacturaBO
    {
        public static List<eDetalleFactura> getDetalleByFactura(string pConnection,Guid id_factura)
        {
            List<eDetalleFactura> lDetalle = new List<eDetalleFactura>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter(string.Concat("SELECT d.id , p.nombre, d.cantidad_productos, d.total from tb_detalles_factura as d inner join tb_productos as p on d.id_productos = p.id WHERE id_factura = '", id_factura+"'"), lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            eDetalleFactura lDetalles = new eDetalleFactura();
                            lDetalles.id = Convert.ToInt32(lRow["id"]);
                            lDetalles.cantidad_productos = Convert.ToInt32(lRow["cantidad_productos"]);
                            lDetalles.total = Convert.ToDecimal(lRow["total"]);
                            lDetalles.nombre_producto = Convert.ToString(lRow["nombre"]);
                            lDetalle.Add(lDetalles);
                        }
                    }
                }
            }
            return lDetalle;

        }
    }
}
