using System;

namespace SimpleWebApp.Models
{
    public class MenuItemMedia
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; } // image, video etc.
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
