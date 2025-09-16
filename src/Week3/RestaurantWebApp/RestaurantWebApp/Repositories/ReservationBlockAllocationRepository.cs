using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class ReservationBlockAllocationRepository : RepositoryBase, IReservationBlockAllocationRepository
    {
        public SimpleWebApp.Models.ReservationBlockAllocation GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_block_allocations WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read()) return Map(reader);
                }
            }
            return null;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationBlockAllocation> GetAll()
        {
            var list = new List<SimpleWebApp.Models.ReservationBlockAllocation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_block_allocations", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) list.Add(Map(reader));
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationBlockAllocation> GetByReservationId(int reservationId)
        {
            var list = new List<SimpleWebApp.Models.ReservationBlockAllocation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_block_allocations WHERE reservation_id=@ReservationId", conn))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationBlockAllocation> GetByBlockId(int blockId)
        {
            var list = new List<SimpleWebApp.Models.ReservationBlockAllocation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_block_allocations WHERE block_id=@BlockId", conn))
                {
                    cmd.Parameters.AddWithValue("@BlockId", blockId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.ReservationBlockAllocation entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO reservation_block_allocations (reservation_id, block_id, seats_reserved) 
                      VALUES (@ReservationId, @BlockId, @SeatsReserved)", conn))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", entity.ReservationId);
                    cmd.Parameters.AddWithValue("@BlockId", entity.BlockId);
                    cmd.Parameters.AddWithValue("@SeatsReserved", entity.SeatsReserved);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.ReservationBlockAllocation entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE reservation_block_allocations 
                      SET reservation_id=@ReservationId, block_id=@BlockId, seats_reserved=@SeatsReserved 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@ReservationId", entity.ReservationId);
                    cmd.Parameters.AddWithValue("@BlockId", entity.BlockId);
                    cmd.Parameters.AddWithValue("@SeatsReserved", entity.SeatsReserved);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM reservation_block_allocations WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.ReservationBlockAllocation Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.ReservationBlockAllocation
            {
                Id = (int)reader["id"],
                ReservationId = (int)reader["reservation_id"],
                BlockId = (int)reader["block_id"],
                SeatsReserved = (int)reader["seats_reserved"]
            };
        }
    }
}
