using SimpleWebApp.Models;
using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IReservationOrderRepository : IRepository<SimpleWebApp.Models.ReservationOrder>
    {
        IEnumerable<SimpleWebApp.Models.ReservationOrder> GetByGuestId(int guestId);
    }
}
