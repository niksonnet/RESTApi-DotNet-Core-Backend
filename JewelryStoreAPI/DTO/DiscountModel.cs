namespace JewelryStoreAPI.DTO
{
    public class DiscountModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public decimal Percentage { get; set; }

        public UserModel User { get; set; }
    }
}
