namespace projAndreTurismoEF.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? District { get; set; }
        public string? ZipCode { get; set; }
        public string? Complement { get; set; }
        public City City { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
