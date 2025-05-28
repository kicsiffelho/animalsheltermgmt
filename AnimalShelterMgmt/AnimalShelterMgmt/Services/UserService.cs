using AnimalShelterMgmt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AnimalShelterMgmt.Services
{
    public class UserService
    {
        private const string ConnectionString = "server=localhost;user id=root;password=;database=animalsheltermgmt;SslMode=none;";

        public User? GetUserById(string auth0id)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM users WHERE auth0id = @auth0id", conn);
            cmd.Parameters.AddWithValue("@auth0id", auth0id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Auth0Id = reader.GetString("auth0id"),
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

            var cmd = new MySqlCommand("INSERT INTO users (auth0id, role, created_at) VALUES (@auth0id, @role, @created_at)", conn);

            cmd.Parameters.AddWithValue("@auth0id", user.Auth0Id);
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
                    Auth0Id = reader.GetString("auth0id"),
                    Role = reader.GetString("role"),
                    CreatedAt = reader.GetDateTime("created_at")
                });
            }

            return users;
        }
    }
}
