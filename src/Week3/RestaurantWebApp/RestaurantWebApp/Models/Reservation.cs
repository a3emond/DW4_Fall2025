using System;

namespace SimpleWebApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Type { get; set; }   // DineIn, Preorder, Delivery, Estimate
        public string Status { get; set; } // Pending, Confirmed, Cancelled etc.
        public DateTime CreatedAt { get; set; }
    }
}
