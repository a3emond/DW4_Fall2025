namespace SimpleWebApp.Models
{
    public class ReservationOrder
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // snapshot at reservation time
    }
}
