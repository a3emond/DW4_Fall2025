using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class ReservationTimeblockRepository : RepositoryBase, IReservationTimeblockRepository
    {
        public SimpleWebApp.Models.ReservationTimeblock GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_timeblocks WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read()) return Map(reader);
                }
            }
            return null;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationTimeblock> GetAll()
        {
            var list = new List<SimpleWebApp.Models.ReservationTimeblock>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_timeblocks", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) list.Add(Map(reader));
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationTimeblock> GetByDate(DateTime date)
        {
            var list = new List<SimpleWebApp.Models.ReservationTimeblock>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_timeblocks WHERE block_date=@Date", conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.ReservationTimeblock entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO reservation_timeblocks (block_date, block_start, block_end, capacity) 
                      VALUES (@Date, @Start, @End, @Capacity)", conn))
                {
                    cmd.Parameters.AddWithValue("@Date", entity.BlockDate.Date);
                    cmd.Parameters.AddWithValue("@Start", entity.BlockStart);
                    cmd.Parameters.AddWithValue("@End", entity.BlockEnd);
                    cmd.Parameters.AddWithValue("@Capacity", entity.Capacity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.ReservationTimeblock entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE reservation_timeblocks 
                      SET block_date=@Date, block_start=@Start, block_end=@End, capacity=@Capacity 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Date", entity.BlockDate.Date);
                    cmd.Parameters.AddWithValue("@Start", entity.BlockStart);
                    cmd.Parameters.AddWithValue("@End", entity.BlockEnd);
                    cmd.Parameters.AddWithValue("@Capacity", entity.Capacity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM reservation_timeblocks WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.ReservationTimeblock Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.ReservationTimeblock
            {
                Id = (int)reader["id"],
                BlockDate = (DateTime)reader["block_date"],
                BlockStart = (TimeSpan)reader["block_start"],
                BlockEnd = (TimeSpan)reader["block_end"],
                Capacity = (int)reader["capacity"],
                CreatedAt = (DateTime)reader["created_at"]
            };
        }
    }
}
