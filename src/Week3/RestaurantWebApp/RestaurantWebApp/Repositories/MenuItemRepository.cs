// MenuItemRepository.cs
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SimpleWebApp.Repositories
{
    public class MenuItemRepository : RepositoryBase, IMenuItemRepository
    {
        public Models.MenuItem GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_items WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return Map(reader);
                    }
                }
            }
            return null;
        }

        public IEnumerable<Models.MenuItem> GetAll()
        {
            var list = new List<Models.MenuItem>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_items", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(Map(reader));
                }
            }
            return list;
        }

        public IEnumerable<Models.MenuItem> GetByCategory(string category)
        {
            var list = new List<Models.MenuItem>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_items WHERE category=@Category", conn))
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(Map(reader));
                    }
                }
            }
            return list;
        }

        public void Insert(Models.MenuItem entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO menu_items (name, category, description, price, is_active) 
                      VALUES (@Name, @Category, @Description, @Price, @IsActive)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Category", entity.Category);
                    cmd.Parameters.AddWithValue("@Description", (object)entity.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Models.MenuItem entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE menu_items 
                      SET name=@Name, category=@Category, description=@Description, 
                          price=@Price, is_active=@IsActive, updated_at=GETDATE() 
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Category", entity.Category);
                    cmd.Parameters.AddWithValue("@Description", (object)entity.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", entity.Price);
                    cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM menu_items WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Models.MenuItem Map(SqlDataReader reader)
        {
            return new Models.MenuItem
            {
                Id = (int)reader["id"],
                Name = (string)reader["name"],
                Category = (string)reader["category"],
                Description = reader["description"] as string,
                Price = (decimal)reader["price"],
                IsActive = (bool)reader["is_active"],
                CreatedAt = (DateTime)reader["created_at"],
                UpdatedAt = (DateTime)reader["updated_at"]
            };
        }

    }
}
