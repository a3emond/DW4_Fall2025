using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class ReservationGuestRepository : RepositoryBase, IReservationGuestRepository
    {
        public SimpleWebApp.Models.ReservationGuest GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_guests WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read()) return Map(reader);
                }
            }
            return null;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationGuest> GetAll()
        {
            var list = new List<SimpleWebApp.Models.ReservationGuest>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_guests", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) list.Add(Map(reader));
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationGuest> GetByReservationId(int reservationId)
        {
            var list = new List<SimpleWebApp.Models.ReservationGuest>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_guests WHERE reservation_id=@ReservationId", conn))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.ReservationGuest entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO reservation_guests (reservation_id, guest_name) 
                      VALUES (@ReservationId, @GuestName)", conn))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", entity.ReservationId);
                    cmd.Parameters.AddWithValue("@GuestName", (object)entity.GuestName ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.ReservationGuest entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE reservation_guests SET reservation_id=@ReservationId, guest_name=@GuestName 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@ReservationId", entity.ReservationId);
                    cmd.Parameters.AddWithValue("@GuestName", (object)entity.GuestName ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM reservation_guests WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.ReservationGuest Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.ReservationGuest
            {
                Id = (int)reader["id"],
                ReservationId = (int)reader["reservation_id"],
                GuestName = reader["guest_name"] as string
            };
        }
    }
}
