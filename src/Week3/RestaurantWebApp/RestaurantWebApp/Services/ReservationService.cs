using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class ReservationService : ServiceBase<Reservation>, IReservationService
    {
        private readonly IReservationRepository _repo;

        public ReservationService() : this(new ReservationRepository()) { }

        public ReservationService(IReservationRepository repo)
        {
            _repo = repo;
        }

        public override Reservation GetById(int id) => _repo.GetById(id);
        public override IEnumerable<Reservation> GetAll() => _repo.GetAll();
        public override void Create(Reservation entity) => _repo.Insert(entity);
        public override void Update(Reservation entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);

        public IEnumerable<Reservation> GetByUser(int userId) => _repo.GetByUserId(userId);
    }
}
