using System.Collections.Generic;

namespace SimpleWebApp.Repositories
{
    public interface IReservationTimeblockRepository : IRepository<SimpleWebApp.Models.ReservationTimeblock>
    {
        IEnumerable<SimpleWebApp.Models.ReservationTimeblock> GetByDate(System.DateTime date);
    }
}
