using AnimalShelterMgmt.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.Services
{
    public class DatabaseService
    {
        private const string ConnectionString = "server=localhost;user id=root;password=;database=animalsheltermgmt;SslMode=none;";

        public List<Animal> GetAnimals()
        {
            var animals = new List<Animal>();

            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT name, species, age, description, image_url FROM animals", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                animals.Add(new Animal
                {
                    Name = reader.GetString("name"),
                    Species = reader.GetString("species"),
                    Age = reader.GetInt32("age"),
                    Description = reader.GetString("description"),
                    ImageUrl = reader.GetString("image_url")
                });
            }
            return animals;
        }
        public bool AddAnimal(Animal animal)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO animals (name, species, age, description, image_url) VALUES (@name, @species, @age, @description, @image)", conn);

            cmd.Parameters.AddWithValue("@name", animal.Name);
            cmd.Parameters.AddWithValue("@species", animal.Species);
            cmd.Parameters.AddWithValue("@age", animal.Age);
            cmd.Parameters.AddWithValue("@description", animal.Description);
            cmd.Parameters.AddWithValue("@image", animal.ImageUrl);

            return cmd.ExecuteNonQuery() == 1;
        }
        public void StoreUser(string email, string role)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO users (email, role) VALUES (@email, @role)", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.ExecuteNonQuery();
        }
        public string GetUserRole(string email)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT role FROM users WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? reader.GetString("role") : "guest";
        }
        public User SaveOrGetUser(string auth0Id, string defaultRole = "foster")
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            // 1. Meglévő lekérése
            var checkCmd = new MySqlCommand("SELECT id, auth0id, role FROM users WHERE auth0id = @auth0id", conn);
            checkCmd.Parameters.AddWithValue("@auth0id", auth0Id);

            using var reader = checkCmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Auth0Id = reader["auth0id"].ToString(),
                    Role = reader["role"].ToString()
                };
            }

            reader.Close();

            // 2. Nincs még ilyen, beszúrjuk
            var insertCmd = new MySqlCommand("INSERT INTO users (auth0id, role) VALUES (@auth0id, @role)", conn);
            insertCmd.Parameters.AddWithValue("@auth0id", auth0Id);
            insertCmd.Parameters.AddWithValue("@role", defaultRole);
            insertCmd.ExecuteNonQuery();

            // 3. Új user visszaadása
            return new User
            {
                Auth0Id = auth0Id,
                Role = defaultRole
            };
        }
    }
}
