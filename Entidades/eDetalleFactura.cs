using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eDetalleFactura
    {
        public int id { get; set; }
        public int cantidad_productos { get; set; }
        public decimal total { get; set; }
        public string id_factura { get; set; }
        public int id_productos { get; set; }
    }
}
