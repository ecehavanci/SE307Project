using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


namespace SE307Project
{
    public class Database
    {
        public List<User> UserList;

        public Database()
        {
            UserList = new List<User>();
        }
        
        public User LogIn(string email, string password){
            foreach (User user in UserList) {
                if (user._Email == email)
                {
                    if (user._Password == password) {
                        return user;
                    }
                    throw new ExceptionWrongPassword(password);
                }

                throw new ExceptionWrongEmail(email);
            }
            return null;
        }

        public void RegisterPetOwner(string name,string surname,string email,string password,string location)
        {
            /*  try
              {
                  CheckUserExists(email); 
              }
              catch (ExceptionAlreadyExistEmail e)
              {
                  e.PrintException();
              }*/

            User newPetOwner = new PetOwner(name, surname, email, password);
            FileHandler xmlHandler = new FileHandler();
            xmlHandler.WriteUser("petOwner.xml", newPetOwner);
            UserList.Add(newPetOwner);
            Console.WriteLine("Registration successful.");
            
        }
        
        public void RegisterPetSitter(string name,string surname,string email,string password,string location)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            User newPetSitter = new PetSitter(name, surname, email, password);
            UserList.Add(newPetSitter);
            Console.WriteLine("Registration successful.");
            //FileHandler xmlHandler = new FileHandler();
            //xmlHandler.WriteUser("petSitter.xml", newPetSitter);
            using (XmlWriter writer = XmlWriter.Create("petSitter.xml"))
            {
                serializer.Serialize(writer, newPetSitter);
            }
        } 
        
        private void CheckUserExists(string email) { //TODO: Implement it
            foreach (User user in UserList) {
                if (user._Email == email) {
                    throw new ExceptionAlreadyExistEmail(email);
                }
            }
        }

        public void ListUsers()
        {
            foreach (var user in UserList)
            {
                Console.WriteLine(user.ToString());
            }
        }
        
        public List<PetSitter> ListPetSitters()
        {
            int index = 1;
            List<PetSitter> list = new List<PetSitter>();
            foreach (var user in UserList)
            {
                if (user is PetSitter)
                {
                    Console.WriteLine( index+ ") " + user.ToString());
                    list.Add((PetSitter) user);
                    index++;
                }
            }

            return list;
        }

    }
    /* private readonly string _connectionString;
        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertPetOwner(string name,string surname,string email,string password,string location)
        {
            string insertSql = "INSERT INTO users (name, surname, email, password, location) VALUES (@name, @surname, @email, @password, @location)";
            

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(insertSql, connection))
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
            

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(insertSql, connection))
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

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        } */
}