using System.Collections.Generic;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IReservationOrderService : IService<ReservationOrder>
    {
        IEnumerable<ReservationOrder> GetByGuest(int guestId);
    }
}
