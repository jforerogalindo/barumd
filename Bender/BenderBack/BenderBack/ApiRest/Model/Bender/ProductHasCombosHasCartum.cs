using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class ProductHasCombosHasCartum
    {
        public int ProductHasComboIdproductcombo { get; set; }
        public int? ProductHasComboProductIdproduct { get; set; }
        public int? ProductHasComboComboIdcombo { get; set; }
        public int? MenuIdmenu { get; set; }
    }
}
