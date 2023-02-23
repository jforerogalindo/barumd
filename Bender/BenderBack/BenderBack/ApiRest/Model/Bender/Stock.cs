using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Stock
    {
        public int Idstock { get; set; }
        public int? Stock1 { get; set; }
        public DateOnly? Date { get; set; }
        public int? ProductIdproduct { get; set; }
    }
}
