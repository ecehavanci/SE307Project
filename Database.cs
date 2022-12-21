using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Database
    {
        public List<User> UserList = new List<User>();
        public User LogIn(string email, string password){
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
        }

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