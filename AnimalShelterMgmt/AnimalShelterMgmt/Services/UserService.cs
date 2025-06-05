using AnimalShelterMgmt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AnimalShelterMgmt.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private const string ConnectionString = "server=localhost;user id=root;password=;database=animalsheltermgmt;SslMode=none;";
        public UserService()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddDebug() // Logs to Output window in Visual Studio
                    .SetMinimumLevel(LogLevel.Information);
            });

            _logger = loggerFactory.CreateLogger<UserService>();
        }
        public User? GetUserById(string auth0id)
        {
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                _logger.LogInformation("Opened DB connection to retrieve user with Auth0 ID: {Auth0Id}", auth0id);
                var cmd = new MySqlCommand("SELECT * FROM users WHERE auth0id = @auth0id", conn);
                cmd.Parameters.AddWithValue("@auth0id", auth0id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _logger.LogInformation("User found for Auth0 ID: {Auth0Id}", auth0id);
                    return new User
                    {
                        Auth0Id = reader.GetString("auth0id"),
                        Role = reader.GetString("role"),
                        CreatedAt = reader.GetDateTime("created_at")
                    };
                }
                _logger.LogWarning("No user found for Auth0 ID: {Auth0Id}", auth0id);
                return null;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error retrieving user with Auth0 ID: {Auth0Id}", auth0id);
                throw;
            }
        }

        public bool RegisterUser(User user)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            _logger.LogInformation("Registering new user with Auth0 ID: {Auth0Id}", user.Auth0Id);
            var cmd = new MySqlCommand("INSERT INTO users (auth0id, role, created_at) VALUES (@auth0id, @role, @created_at)", conn);

            cmd.Parameters.AddWithValue("@auth0id", user.Auth0Id);
            cmd.Parameters.AddWithValue("@role", user.Role);
            cmd.Parameters.AddWithValue("@created_at", user.CreatedAt);

            return cmd.ExecuteNonQuery() == 1;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
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
                _logger.LogInformation("Retrieved {UserCount} users", users.Count);
                return users;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw;
            }
        }
    }
}
