using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class ReservationRepository : RepositoryBase, IReservationRepository
    {
        public SimpleWebApp.Models.Reservation GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservations WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read()) return Map(reader);
                }
            }
            return null;
        }

        public IEnumerable<SimpleWebApp.Models.Reservation> GetAll()
        {
            var list = new List<SimpleWebApp.Models.Reservation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservations", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) list.Add(Map(reader));
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.Reservation> GetByUserId(int userId)
        {
            var list = new List<SimpleWebApp.Models.Reservation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservations WHERE user_id=@UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.Reservation> GetByStatus(string status)
        {
            var list = new List<SimpleWebApp.Models.Reservation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservations WHERE status=@Status", conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.Reservation entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO reservations (user_id, reservation_date, type, status) 
                      VALUES (@UserId, @Date, @Type, @Status)", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                    cmd.Parameters.AddWithValue("@Date", entity.ReservationDate);
                    cmd.Parameters.AddWithValue("@Type", entity.Type);
                    cmd.Parameters.AddWithValue("@Status", entity.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.Reservation entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE reservations 
                      SET user_id=@UserId, reservation_date=@Date, type=@Type, status=@Status 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                    cmd.Parameters.AddWithValue("@Date", entity.ReservationDate);
                    cmd.Parameters.AddWithValue("@Type", entity.Type);
                    cmd.Parameters.AddWithValue("@Status", entity.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM reservations WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.Reservation Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.Reservation
            {
                Id = (int)reader["id"],
                UserId = (int)reader["user_id"],
                ReservationDate = (DateTime)reader["reservation_date"],
                Type = (string)reader["type"],
                Status = (string)reader["status"],
                CreatedAt = (DateTime)reader["created_at"]
            };
        }
    }
}
