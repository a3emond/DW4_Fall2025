using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IReservationBlockAllocationRepository : IRepository<SimpleWebApp.Models.ReservationBlockAllocation>
    {
        IEnumerable<SimpleWebApp.Models.ReservationBlockAllocation> GetByReservationId(int reservationId);
        IEnumerable<SimpleWebApp.Models.ReservationBlockAllocation> GetByBlockId(int blockId);
    }
}
