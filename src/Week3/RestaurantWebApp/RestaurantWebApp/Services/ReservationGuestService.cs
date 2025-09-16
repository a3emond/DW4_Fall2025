using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class ReservationGuestService : ServiceBase<ReservationGuest>, IReservationGuestService
    {
        private readonly IReservationGuestRepository _repo;

        public ReservationGuestService() : this(new ReservationGuestRepository()) { }

        public ReservationGuestService(IReservationGuestRepository repo)
        {
            _repo = repo;
        }

        public override ReservationGuest GetById(int id) => _repo.GetById(id);
        public override IEnumerable<ReservationGuest> GetAll() => _repo.GetAll();
        public override void Create(ReservationGuest entity) => _repo.Insert(entity);
        public override void Update(ReservationGuest entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);
    }
}
