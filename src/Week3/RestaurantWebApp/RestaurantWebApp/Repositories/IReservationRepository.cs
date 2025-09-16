using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IReservationRepository : IRepository<SimpleWebApp.Models.Reservation>
    {
        IEnumerable<SimpleWebApp.Models.Reservation> GetByUserId(int userId);
        IEnumerable<SimpleWebApp.Models.Reservation> GetByStatus(string status);
    }
}
