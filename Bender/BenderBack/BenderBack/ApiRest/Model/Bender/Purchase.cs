using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Purchase
    {
        public int IdPurchase { get; set; }
        public DateOnly? Date { get; set; }
        public string? Supplier { get; set; }
        public string? Nitsupplier { get; set; }
        public string? Quantity { get; set; }
        public int? ProductIdproduct { get; set; }
    }
}
