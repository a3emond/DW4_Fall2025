using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public class MenuItemMediaRepository : RepositoryBase, IMenuItemMediaRepository
    {
        public SimpleWebApp.Models.MenuItemMedia GetById(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_item_media WHERE id=@Id", conn))
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

        public IEnumerable<SimpleWebApp.Models.MenuItemMedia> GetAll()
        {
            var list = new List<SimpleWebApp.Models.MenuItemMedia>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_item_media", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(Map(reader));
                }
            }
            return list;
        }

        public IEnumerable<SimpleWebApp.Models.MenuItemMedia> GetByMenuItemId(int menuItemId)
        {
            var list = new List<SimpleWebApp.Models.MenuItemMedia>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM menu_item_media WHERE menu_item_id=@MenuItemId", conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(Map(reader));
                    }
                }
            }
            return list;
        }

        public void Insert(SimpleWebApp.Models.MenuItemMedia entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"INSERT INTO menu_item_media (menu_item_id, file_name, media_type, is_primary) 
                      VALUES (@MenuItemId, @FileName, @MediaType, @IsPrimary)", conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemId", entity.MenuItemId);
                    cmd.Parameters.AddWithValue("@FileName", entity.FileName);
                    cmd.Parameters.AddWithValue("@MediaType", entity.MediaType);
                    cmd.Parameters.AddWithValue("@IsPrimary", entity.IsPrimary);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(SimpleWebApp.Models.MenuItemMedia entity)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"UPDATE menu_item_media 
                      SET menu_item_id=@MenuItemId, file_name=@FileName, media_type=@MediaType, is_primary=@IsPrimary
                      WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@MenuItemId", entity.MenuItemId);
                    cmd.Parameters.AddWithValue("@FileName", entity.FileName);
                    cmd.Parameters.AddWithValue("@MediaType", entity.MediaType);
                    cmd.Parameters.AddWithValue("@IsPrimary", entity.IsPrimary);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM menu_item_media WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private SimpleWebApp.Models.MenuItemMedia Map(SqlDataReader reader)
        {
            return new SimpleWebApp.Models.MenuItemMedia
            {
                Id = (int)reader["id"],
                MenuItemId = (int)reader["menu_item_id"],
                FileName = (string)reader["file_name"],
                MediaType = (string)reader["media_type"],
                IsPrimary = (bool)reader["is_primary"],
                CreatedAt = (DateTime)reader["created_at"]
            };
        }
    }
}
