using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Order
    {
        public int Idorder { get; set; }
        public DateTime? Date { get; set; }
        public int? Iduser { get; set; }
        public int? Idproduct { get; set; }
        public int? MesaIdmesa { get; set; }
        public int? MesaSucursalIdsucursal { get; set; }
        public int? CombosIdcombos { get; set; }
    }
}
