using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class ReservationTimeblockService : ServiceBase<ReservationTimeblock>, IReservationTimeblockService
    {
        private readonly IReservationTimeblockRepository _repo;

        public ReservationTimeblockService() : this(new ReservationTimeblockRepository()) { }

        public ReservationTimeblockService(IReservationTimeblockRepository repo)
        {
            _repo = repo;
        }

        public override ReservationTimeblock GetById(int id) => _repo.GetById(id);
        public override IEnumerable<ReservationTimeblock> GetAll() => _repo.GetAll();
        public override void Create(ReservationTimeblock entity) => _repo.Insert(entity);
        public override void Update(ReservationTimeblock entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);
    }
}
