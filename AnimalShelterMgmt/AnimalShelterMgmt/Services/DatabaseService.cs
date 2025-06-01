using AnimalShelterMgmt.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AnimalShelterMgmt.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Windows;


namespace AnimalShelterMgmt.Services
{
    public class DatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private const string ConnectionString = "server=localhost;user id=root;password=;database=animalsheltermgmt;SslMode=none;";

        public string? GetUserAuth0Id(string auth0id)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT auth0id FROM users WHERE auth0id = @auth0id", conn);
            cmd.Parameters.AddWithValue("@auth0id", auth0id);

            object? result = cmd.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                return null;
            }

            return result.ToString();
        }


        public DatabaseService()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddDebug()
                    .SetMinimumLevel(LogLevel.Information);
            });

            _logger = loggerFactory.CreateLogger<DatabaseService>();
        }
        public List<Animal> GetAnimals()
        {
            var animals = new List<Animal>();
            var addedIds = new HashSet<int>();

            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                _logger.LogInformation("Opened connection to database for GetAnimals");

                var cmd = new MySqlCommand(@"SELECT a.id, a.name, a.species, a.age, a.description, a.image_url, a.status, u.auth0id
                                            FROM animals a LEFT JOIN animal_user au ON a.id = au.animal_id
                                            AND au.end_date IS NULL LEFT JOIN users u ON au.user_id = u.auth0id;", conn);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int animalId = reader.GetInt32("id");
                    if (addedIds.Contains(animalId))
                        continue;

                    addedIds.Add(animalId);

                    animals.Add(new Animal
                    {
                        Id = animalId,
                        Name = reader.GetString("name"),
                        Species = reader.GetString("species"),
                        Age = reader.GetInt32("age"),
                        Description = reader.GetString("description"),
                        ImageUrl = reader.GetString("image_url"),
                        Status = reader.GetString("status") switch
                        {
                            "foster" => "fostered",
                            "adopt" => "adopted",
                            var s => s
                        },
                        CurrentUserAuth0Id = reader.IsDBNull(reader.GetOrdinal("auth0id")) ? null : reader.GetString("auth0id")
                    });
                }

                _logger.LogInformation("Retrieved {Count} animals", animals.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching animals");
                throw;
            }

            return animals;
        }



        public bool AddAnimal(Animal animal)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            _logger.LogInformation("Opened connection to database for AddAnimal");
            var cmd = new MySqlCommand("INSERT INTO animals (name, species, age, description, image_url) VALUES (@name, @species, @age, @description, @image)", conn);

            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@age", animal.Age);
            cmd.Parameters.AddWithValue("@description", animal.Description);
            cmd.Parameters.AddWithValue("@image", animal.ImageUrl);
            _logger.LogInformation("Adding new animal: {Name}, {Species}", animal.Name, animal.Species);
            return cmd.ExecuteNonQuery() == 1;
        }

        public void SetUserRole(string auth0id, string role)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            _logger.LogInformation("Opened connection to database for SetUserRole");
            var cmd = new MySqlCommand("UPDATE users SET role = @role WHERE auth0id = @auth0id", conn);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@auth0id", auth0id);
            _logger.LogInformation("Updating role for user {Auth0Id} to {Role}", auth0id, role);
            cmd.ExecuteNonQuery();
        }

        public List<Animal> GetAnimalsByUser(string auth0id)
        {
            var myanimals = new List<Animal>();
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                _logger.LogInformation("Opened connection to database for GetAnimalsByUser");

                var cmd = new MySqlCommand(
                    "SELECT a.id, a.name, a.species, a.age, a.description, a.image_url FROM animals a " +
                    "INNER JOIN animal_user au ON a.id = au.animal_id " +
                    "INNER JOIN users u ON au.user_id = u.auth0id " + 
                    "WHERE u.auth0id = @auth0id", conn);

                cmd.Parameters.AddWithValue("@auth0id", auth0id);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    myanimals.Add(new Animal
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Species = reader.GetString("species"),
                        Age = reader.GetInt32("age"),
                        Description = reader.GetString("description"),
                        ImageUrl = reader.GetString("image_url")
                    });
                }
                _logger.LogInformation("Fetching animals for user {Auth0Id}", auth0id);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while fetching animals by user");
                throw;
            }
            return myanimals;
        }

        public void SetAnimalStatus(int animal_id, string auth0id, string type)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            _logger.LogInformation("Opened connection to database for SetAnimalStatus");

            var cmd = new MySqlCommand(
                "INSERT INTO animal_user (animal_id, user_id, relation_type, start_date) " +
                "VALUES (@animal_id, @user_id, @relation_type, @start_date) " +
                "ON DUPLICATE KEY UPDATE relation_type = VALUES(relation_type), start_date = VALUES(start_date)",
                conn);

            cmd.Parameters.AddWithValue("@animal_id", animal_id);
            cmd.Parameters.AddWithValue("@user_id", auth0id);
            cmd.Parameters.AddWithValue("@relation_type", type);
            cmd.Parameters.AddWithValue("@start_date", DateTime.Now);

            _logger.LogInformation("Állapot beállítva: {Type}, állat: {AnimalId}, user: {UserId}", type, animal_id, auth0id);

            cmd.ExecuteNonQuery();

            var updateStatusCmd = new MySqlCommand(
                "UPDATE animals SET status = @status WHERE id = @animal_id", conn);
            updateStatusCmd.Parameters.AddWithValue("@status", type == "owner" ? "adopted" : "fostered");
            updateStatusCmd.Parameters.AddWithValue("@animal_id", animal_id);
            updateStatusCmd.ExecuteNonQuery();
        }
        public bool DeleteAnimal(int animalId, string adminAuth0Id)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var checkCmd = new MySqlCommand("SELECT role FROM users WHERE auth0id = @auth0id", conn);
            checkCmd.Parameters.AddWithValue("@auth0id", adminAuth0Id);
            var roleObj = checkCmd.ExecuteScalar();

            if (roleObj == null || roleObj == DBNull.Value || roleObj.ToString() != "Admin")
                return false;

            var delAnimalUserCmd = new MySqlCommand("DELETE FROM animal_user WHERE animal_id = @animalId", conn);
            delAnimalUserCmd.Parameters.AddWithValue("@animalId", animalId);
            delAnimalUserCmd.ExecuteNonQuery();

            var delAnimalCmd = new MySqlCommand("DELETE FROM animals WHERE id = @animalId", conn);
            delAnimalCmd.Parameters.AddWithValue("@animalId", animalId);
            int affectedRows = delAnimalCmd.ExecuteNonQuery();

            return affectedRows > 0;
        }
    }
}
