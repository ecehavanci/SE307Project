using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SE307Project
{
    public class Database
    {
        
        public List<PetOwner> PetOwnerList = new List<PetOwner>();
        public List<PetSitter> PetSitterList = new List<PetSitter>();

        public readonly String XmlOwnerFileName = "lastTryO19.xml";
        public readonly String XmlSitterFileName = "lastTryS19.xml";


        private readonly string _connectionString;

        private Database()
        {
        }

        private static Database Instance;

        public static Database GetInstance()
        {
            if (Instance==null)
            {
                Instance = new Database();
            }

            return Instance;
        }
        
        

        //This is for trying TODO: DELETE LATER
        public PetOwner AddPetOwner(string name, string surname, string email, string password)
        {
            var po = new PetOwner(name, surname, email, password);
            PetOwnerList.Add(po);
            return po;
        }

        public PetSitter AddPetSitter(string name, string surname, string email, string password)
        {
            var ps = new PetSitter(name, surname, email, password);
            PetSitterList.Add(ps);
            return ps;
        }

        public void InsertPetOwner(string name, string surname, string email, string password/*, string location*/)
        {
            /*string insertSql =
                "INSERT INTO users (name, surname, email, password, location) VALUES (@name, @surname, @email, @password, @location)";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
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
            }*/
            
            PetOwner po = new PetOwner(name, surname, email, password);
            PetOwnerList.Add(po);
            XMLHandler xmlHandler = new XMLHandler();
            xmlHandler.WritePetOwnerList(XmlOwnerFileName,PetOwnerList);

        }

        public void InsertPetSitter(string name, string surname, string email, string password)
        {
            /*string insertSql = "INSERT INTO users (name, surname, email, password, location) VALUES (@name, @surname, @email, @password, @location)";
            

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
            
            
            }*/

            PetSitter ps = new PetSitter(name, surname, email, password);
            PetSitterList.Add(ps);
            XMLHandler xmlHandler = new XMLHandler();
            xmlHandler.WritePetSitterList(XmlSitterFileName,PetSitterList);
        }

        /*public bool CheckUserExist(string email, string password)
        {
            string selectSql = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    int count = (int) command.ExecuteScalar();
                    return count > 0;
                }
            }
        }*/

        public User FindUser(string email, string password)
        {
            foreach (var user in PetOwnerList)
            {
                if (user.Email == email)
                {
                    if (user.Password!= password)
                    {
                        throw new ExceptionWrongPassword();
                    }
                    return user;
                }
            }

            foreach (var user in PetSitterList)
            {
                if (user.Email == email)
                {
                    if (user.Password!= password)
                    {
                        throw new ExceptionWrongPassword();
                    }
                    return user;
                }
            }

            return null;
        }

        
        public PetOwner FindPetOwner(string email)
        {
            foreach (var user in PetOwnerList)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
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


        public List<PetSitter> ListPetSitters()
        {
            int index = 1;
            List<PetSitter> list = new List<PetSitter>();
            foreach (var user in PetSitterList)
            {
                Console.WriteLine(index + ") ");
                user.ShowProfile();
                list.Add(user);
                index++;
            }

            return list;
        }
        
        
        public List<PetOwner> ListPetOwners()
        {
            int index = 1;
            List<PetOwner> list = new List<PetOwner>();
            foreach (var user in PetOwnerList)
            {
                Console.WriteLine(index + ") ");
                user.ShowProfile();
                list.Add(user);
                index++;
            }

            return list;
        }
    }
}