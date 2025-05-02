using AnimalShelterMgmt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AnimalShelterMgmt.Services
{
    public class UserService
    {
        private const string ConnectionString = "server=localhost;user id=root;password=;database=animalsheltermgmt;SslMode=none;";

        public User? GetUserByUsername(string username)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM users WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("password_hash"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role"),
                    CreatedAt = reader.GetDateTime("created_at")
                };
            }

            return null;
        }

        public bool RegisterUser(User user)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO users (username, password_hash, email, role, created_at) VALUES (@username, @password_hash, @email, @role, @created_at)", conn);

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password_hash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.Parameters.AddWithValue("@created_at", user.CreatedAt);

            return cmd.ExecuteNonQuery() == 1;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM users", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("password_hash"),
                    Email = reader.GetString("email"),
                    Role = reader.GetString("role"),
                    CreatedAt = reader.GetDateTime("created_at")
                });
            }

            return users;
        }
    }
}
