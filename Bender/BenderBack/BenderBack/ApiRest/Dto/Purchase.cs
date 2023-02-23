namespace ApiRest.Dto.Purchase
{
    public partial class Insert
    {        
        public string? Identification { get; set; }
        public string? Name { get; set; }
    }
    public partial class GetData
    {
        public int Id { get; set; }
        public string? Identification { get; set; }
        public string? Name { get; set; }
    }

    public partial class Edit
    {
        public int Id { get; set; }
        public string? Identification { get; set; }
        public string? Name { get; set; }
    }
}
