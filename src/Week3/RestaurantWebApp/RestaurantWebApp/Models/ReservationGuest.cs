namespace SimpleWebApp.Models
{
    public class ReservationGuest
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string GuestName { get; set; }
    }
}
