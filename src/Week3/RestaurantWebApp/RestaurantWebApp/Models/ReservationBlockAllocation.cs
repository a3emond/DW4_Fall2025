namespace SimpleWebApp.Models
{
    public class ReservationBlockAllocation
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int BlockId { get; set; }
        public int SeatsReserved { get; set; }
    }
}
