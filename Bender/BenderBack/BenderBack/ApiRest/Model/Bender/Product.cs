using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Product
    {
        public int Idproduct { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Price { get; set; }
        public int? InvoiceIdinvoice { get; set; }
    }
}
