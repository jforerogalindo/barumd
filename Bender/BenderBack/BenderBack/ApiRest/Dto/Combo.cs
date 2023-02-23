namespace ApiRest.Dto.Combo
{
    public class Insert
    {
        public string nameCombo { get; set; }
        public List<Dto.Product.ProductCombo> Products {get;set;}
    }

    public class GetData
    {
        public int Idcombo { get; set; }
        public string? nameCombo { get; set; }
        public List<Dto.Product.ProductCombo>? Products { get; set; }
    }

    public class Edit
    {
        public string nameCombo { get; set; }
        public List<Dto.Product.ProductCombo> Products { get; set; }
    }
}
