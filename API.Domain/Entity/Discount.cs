namespace API.Domain.Entity
{
    public class Discount
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public decimal Percentage { get; set; }
        public User User { get; set; }
    }
}
