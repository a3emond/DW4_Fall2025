using System.Collections.Generic;
using SimpleWebApp.Models;
using SimpleWebApp.Repositories;

namespace SimpleWebApp.Services
{
    public class ReservationBlockAllocationService : ServiceBase<ReservationBlockAllocation>, IReservationBlockAllocationService
    {
        private readonly IReservationBlockAllocationRepository _repo;

        public ReservationBlockAllocationService() : this(new ReservationBlockAllocationRepository()) { }

        public ReservationBlockAllocationService(IReservationBlockAllocationRepository repo)
        {
            _repo = repo;
        }

        public override ReservationBlockAllocation GetById(int id) => _repo.GetById(id);
        public override IEnumerable<ReservationBlockAllocation> GetAll() => _repo.GetAll();
        public override void Create(ReservationBlockAllocation entity) => _repo.Insert(entity);
        public override void Update(ReservationBlockAllocation entity) => _repo.Update(entity);
        public override void Delete(int id) => _repo.Delete(id);

        public IEnumerable<ReservationBlockAllocation> GetByReservation(int reservationId) => _repo.GetByReservationId(reservationId);
    }
}
