namespace ApiRest.Dto.Product
{
    public class ProductCombo
    {
        public int Idproduct { get; set; }
    }
    public class Insert
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Price { get; set; }
        public int? InvoiceIdinvoice { get; set; }
    }
    public class GetData
    {
        public int Idproduct { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Price { get; set; }
        public int? InvoiceIdinvoice { get; set; }
    }
    public class Edit
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Price { get; set; }
        public int? InvoiceIdinvoice { get; set; }
    }
}
