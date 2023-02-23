using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class ProductHasCombo
    {
        public int Idproductocombo { get; set; }
        public int? ProductoIdproducto { get; set; }
        public int? CombosIdcombos { get; set; }
    }
}
