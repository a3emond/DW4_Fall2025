using System.Collections.Generic;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IReservationService : IService<Reservation>
    {
        IEnumerable<Reservation> GetByUser(int userId);
    }
}
