using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Menu
    {
        public int Idmenu { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public int? Idstock { get; set; }
        public int? Idproductcombo { get; set; }
    }
}
