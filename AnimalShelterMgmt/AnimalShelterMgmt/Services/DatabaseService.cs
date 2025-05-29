using AnimalShelterMgmt.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AnimalShelterMgmt.Models;
using System.Diagnostics;

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

            var cmd = new MySqlCommand("SELECT id, name, species, age, description, image_url FROM animals", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                animals.Add(new Animal
                {
                    Id = reader.GetInt32("id"),
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

        public void SetUserRole(string auth0id, string role)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("UPDATE users SET role = @role WHERE auth0id = @auth0id", conn);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@auth0id", auth0id);

            cmd.ExecuteNonQuery();
        }

        public List<Animal> GetAnimalsByUser(string auth0id)
        {
            var myanimals = new List<Animal>();

            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT a.id, a.name, a.species, a.age, a.description, a.image_url FROM animals a " +
                "INNER JOIN animal_user au ON a.id = au.animal_id INNER JOIN users u ON au.user_id = u.auth0id " +
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
            return myanimals;
        }

        public void SetAnimalStatus(int animal_id, string auth0id, string type)
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO animal_user (animal_id, user_id, relation_type, start_date) " +
                "SELECT @animal_id, @user_id, @relation_type, @start_date " +
                "ON DUPLICATE KEY UPDATE relation_type = VALUES(relation_type), start_date = VALUES(start_date)", conn);
            cmd.Parameters.AddWithValue("@animal_id", animal_id);
            cmd.Parameters.AddWithValue("@user_id", auth0id);
            cmd.Parameters.AddWithValue("@relation_type", type);
            cmd.Parameters.AddWithValue("@start_date", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
    }
}
