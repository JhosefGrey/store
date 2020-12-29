using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FacturasEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public int num { get; set; }
        public string serie { get; set; }
        public DateTime fecha { get; set; }
        public int id_cliente { get; set; }     

    }
}
