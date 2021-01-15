using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public partial class FacturasBO
    {
        public static List<eFactura> getClienteById(string pConnection)
        {
            List<eFactura> eFactura = new List<eFactura>();
            using (SqlConnection lConnection = new SqlConnection(pConnection))
            {
                using (SqlDataAdapter lDataAdapter = new SqlDataAdapter("SELECT f.id, c.nombre, c.apellido, f.fecha, f.num, f.serie FROM tb_facturas as f INNER JOIN tb_clientes as c ON f.id_cliente =  c.id", lConnection))
                {
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    DataTable lDataTable = new DataTable();
                    lDataAdapter.Fill(lDataTable);
                    if (lDataTable != null && lDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow lRow in lDataTable.Rows)
                        {
                            eFactura lFactura = new eFactura();
                            lFactura.id = Convert.ToString(lRow["id"]);
                            lFactura.nombre_cliente = Convert.ToString(lRow["nombre"]);
                            lFactura.apellido_cliente = Convert.ToString(lRow["apellido"]);
                            lFactura.fecha = Convert.ToDateTime(lRow["fecha"]);
                            lFactura.num = Convert.ToInt32(lRow["num"]);
                            lFactura.serie = Convert.ToString(lRow["serie"]);
                            eFactura.Add(lFactura);
                        }
                    }
                }
            }
            return eFactura;

        }
    }
}
