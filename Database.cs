using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SE307Project
{
    public class Database
    {
        public List<User> UserList = new List<User>();
        
        private readonly string _connectionString;
        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertPetOwner(string name,string surname,string email,string password,string location)
        {
            string insertSql = "INSERT INTO users (name, surname, email, password, location) VALUES (@name, @surname, @email, @password, @location)";
            

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@location", location);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public void InsertPetSitter(string name,string surname,string email,string password,string location)
        {
            string insertSql = "INSERT INTO users (name, surname, email, password, location) VALUES (@name, @surname, @email, @password, @location)";
            

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@location", location);
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool CheckUserExist(string email, string password)
        {
            string selectSql = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        
        
       /* public User LogIn(string email, string password){
            foreach (User user in UserList) {
                if (user._Email == email)
                {
                    if (user._Password == password) {
                        return user;
                    }
                    throw new ExceptionWrongPassword(password);
                } 
            }
            return null;
        }

        public void RegisterPetOwner(string name,string surname,string email,string password,string location)
        {
            User newPetOwner = new PetOwner(name, surname, email, password);
            UserList.Add(newPetOwner);
            Console.WriteLine("Registration successful.");
        }
        
        public void RegisterPetSitter(string name,string surname,string email,string password,string location)
        {
            User newPetSitter = new PetOwner(name, surname, email, password);
            UserList.Add(newPetSitter);
            Console.WriteLine("Registration successful.");
        } */

        public void ListUsers()
        {
            foreach (var user in UserList)
            {
                Console.WriteLine(user.ToString());
            }
        }
        
        public void ListUsers(Char type)
        {
            
        }

    }
}