namespace Crud_Domain.Models
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
        public string UserId { get; set; }
        protected Product() { }


        // This static factory method, named `Create`, belongs to the
        // `Product` model. It constructs and returns a new `Product`
        // instance with the specified properties (name, production date
        // , phone, email, and availability). The method encapsulates
        // object creation logic, promoting readability and consistency in code.
        public static Product Create(string name, DateTime produceDate, int manufacturePhone, string manufactureEmail, bool isAvailable, string userid)
        {
            return new Product
            {
                Name = name,
                ProduceDate = produceDate,
                ManufacturePhone = manufacturePhone,
                ManufactureEmail = manufactureEmail,
                IsAvailable = isAvailable,
                UserId = userid
            };
        }
        public Product(string name, DateTime producedate, int manufacturePhone, string manufactureEmail, string userid)
        {
            Name = name;
            ProduceDate = producedate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = true;
            UserId = userid;
        }
    }
}