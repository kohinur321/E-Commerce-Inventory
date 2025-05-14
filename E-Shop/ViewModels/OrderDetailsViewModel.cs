namespace E_Shop.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int orderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductUnit { get; set; }
        public string CartTotalPrice { get; set; }
        public string CartQuantity { get; set; }
   }
}
