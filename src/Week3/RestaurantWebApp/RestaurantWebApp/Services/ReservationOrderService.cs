using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class ReservationOrderService : ServiceBase<ReservationOrder>, IReservationOrderService
    {
        private readonly IReservationOrderRepository _repo;

        public ReservationOrderService() : this(new ReservationOrderRepository()) { }

        public ReservationOrderService(IReservationOrderRepository repo)
        {
            _repo = repo;
        }

        public override ReservationOrder GetById(int id) => _repo.GetById(id);
        public override IEnumerable<ReservationOrder> GetAll() => _repo.GetAll();
        public override void Create(ReservationOrder entity) => _repo.Insert(entity);
        public override void Update(ReservationOrder entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);

        public IEnumerable<ReservationOrder> GetByGuest(int guestId) => _repo.GetByGuestId(guestId);
    }
}
