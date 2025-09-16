using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class ReservationOrderRepository : RepositoryBase, IReservationOrderRepository
    {
        public SimpleWebApp.Models.ReservationOrder GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_orders WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read()) return Map(reader);
                }
            }
            return null;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationOrder> GetAll()
        {
            var list = new List<SimpleWebApp.Models.ReservationOrder>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_orders", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) list.Add(Map(reader));
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.ReservationOrder> GetByGuestId(int guestId)
        {
            var list = new List<SimpleWebApp.Models.ReservationOrder>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM reservation_orders WHERE guest_id=@GuestId", conn))
                {
                    cmd.Parameters.AddWithValue("@GuestId", guestId);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read()) list.Add(Map(reader));
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.ReservationOrder entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO reservation_orders (guest_id, menu_item_id, quantity, price) 
                      VALUES (@GuestId, @MenuItemId, @Quantity, @Price)", conn))
                {
                    cmd.Parameters.AddWithValue("@GuestId", entity.GuestId);
                    cmd.Parameters.AddWithValue("@MenuItemId", entity.MenuItemId);
                    cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.ReservationOrder entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE reservation_orders 
                      SET guest_id=@GuestId, menu_item_id=@MenuItemId, quantity=@Quantity, price=@Price 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@GuestId", entity.GuestId);
                    cmd.Parameters.AddWithValue("@MenuItemId", entity.MenuItemId);
                    cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM reservation_orders WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.ReservationOrder Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.ReservationOrder
            {
                Id = (int)reader["id"],
                GuestId = (int)reader["guest_id"],
                MenuItemId = (int)reader["menu_item_id"],
                Quantity = (int)reader["quantity"],
                Price = (decimal)reader["price"]
            };
        }

    }
}
