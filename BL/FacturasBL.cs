using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using BO;


namespace BL
{
    public class FacturasBL
    {
        public static void GuardarFactura(string Conection, eFactura pFactura)
        {
            FacturasBO lFactura = new FacturasBO(Conection, pFactura.id);
            if (lFactura.IsNew == true) {
                lFactura.fecha = DateTime.Now;
                lFactura.num = pFactura.num;
            }
            lFactura.serie = pFactura.serie;
            lFactura.id_cliente = pFactura.id_cliente;
            lFactura.Save(Conection);
        }
    }
}
