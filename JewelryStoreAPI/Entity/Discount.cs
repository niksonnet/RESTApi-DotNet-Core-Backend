namespace JewelryStoreAPI.Entity
{
    public class Discount
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public double Percentage { get; set; }
        public User User { get; set; }
    }
}
