﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FacturasEntity
    {
        public int id { get; set; }
        public int num { get; set; }
        public string serie { get; set; }
        public DateTime fecha { get; set; }
        public List<ClientesEntity> id_clientes { get; set; }     
        public DetallesFactura detallesFactura { get; set; }
    }
}
