﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eFactura
    {
        public int id { get; set; } = 0;
        public string serie { get; set; }
        public int id_cliente { get; set; }
        public int num { get; set; }
        public List<eDetalleFactura> Detelle { get; set; }
     
    }
}
