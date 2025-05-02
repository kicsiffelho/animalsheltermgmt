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

    }
}
