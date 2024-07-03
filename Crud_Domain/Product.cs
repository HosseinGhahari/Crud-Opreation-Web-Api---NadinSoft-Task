namespace Crud_Domain
{
    public class Product
    {
        // The Product class models product data, including properties
        // like Id, Name, ProduceDate, ManufacturePhone, ManufactureEmail
        // and IsAvailable. It offers both a default constructor and a
        // parameterized constructor for creating and managing product instances.
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public int ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }

        protected Product() { }

        public static Product Create(string name, DateTime produceDate, int manufacturePhone, string manufactureEmail, bool isAvailable)
        {
            return new Product
            {
                Name = name,
                ProduceDate = produceDate,
                ManufacturePhone = manufacturePhone,
                ManufactureEmail = manufactureEmail,
                IsAvailable = isAvailable
            };
        }
        public Product(string name, DateTime producedate, int manufacturePhone, string manufactureEmail)
        {
            Name = name;
            ProduceDate = producedate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = true;
        }
    }
}