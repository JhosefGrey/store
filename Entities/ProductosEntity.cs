using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductosEntity
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string code { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public DetallesFactura detallesFactura { get; set; }
    }
}
