using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class Invoice
    {
        public int Idinvoice { get; set; }
        public DateOnly? Date { get; set; }
        public string? Paymentmethod { get; set; }
        public int? Idproduct { get; set; }
        public int? Idtable { get; set; }
        public int? Iduser { get; set; }
        public int? Idorder { get; set; }
    }
}
