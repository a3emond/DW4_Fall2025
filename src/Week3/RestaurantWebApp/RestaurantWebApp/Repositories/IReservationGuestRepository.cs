using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IReservationGuestRepository : IRepository<SimpleWebApp.Models.ReservationGuest>
    {
        IEnumerable<SimpleWebApp.Models.ReservationGuest> GetByReservationId(int reservationId);
    }
}
