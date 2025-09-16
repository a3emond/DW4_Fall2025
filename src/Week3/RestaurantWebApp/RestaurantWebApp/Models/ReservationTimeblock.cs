using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApp.Models
{
    public class ReservationTimeblock
    {
        public int Id { get; set; }
        public DateTime BlockDate { get; set; }
        public TimeSpan BlockStart { get; set; }
        public TimeSpan BlockEnd { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}