using System.Collections.Generic;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IReservationBlockAllocationService : IService<ReservationBlockAllocation>
    {
        IEnumerable<ReservationBlockAllocation> GetByReservation(int reservationId);
    }
}
