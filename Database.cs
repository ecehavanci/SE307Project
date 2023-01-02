using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SE307Project
{
    public class Database
    {
        public List<PetOwner> PetOwnerList = new List<PetOwner>();
        public List<PetSitter> PetSitterList = new List<PetSitter>();

        public readonly String XmlOwnerFileName = "PetOwners.xml";
        public readonly String XmlSitterFileName = "PetSitters.xml";


        private readonly string _connectionString;

        private Database()
        {
        }

        private static Database Instance;

        public static Database GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Database();
            }

            return Instance;
        }

        public void InsertPetOwner(string name, string surname, string email, string password /*, string location*/)
        {

            try
            {
                CheckUserExists(email);
                
            PetOwner po = new PetOwner(name, surname, email, password);
            PetOwnerList.Add(po);
            XMLHandler xmlHandler = new XMLHandler();
            xmlHandler.WritePetOwnerList(XmlOwnerFileName,PetOwnerList);
            }
            catch (UserAlreadyExistsException e)
            {
                e.PrintException();
            }
        }

        public void InsertPetSitter(string name, string surname, string email, string password)
        {
          
            try
            {
                CheckUserExists(email); 
                
                PetSitter ps = new PetSitter(name, surname, email, password);
                PetSitterList.Add(ps);
                XMLHandler xmlHandler = new XMLHandler();
                xmlHandler.WritePetSitterList(XmlSitterFileName,PetSitterList);
            }  catch (UserAlreadyExistsException e)
            {
                e.PrintException();
            }
        }


        public User FindUser(string email, string password)
        {
            foreach (var user in PetOwnerList)
            {
                if (user.Email == email)
                {
                    if (user.Password != password)
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
                    if (user.Password != password)
                    {
                        throw new ExceptionWrongPassword();
                    }

                    return user;
                }
            }

            return null;
        }

        public User FindUser(string email)
        {
            foreach (var user in PetOwnerList)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            
            foreach (var user in PetSitterList)
            {
                if (user.Email == email)
                {
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
        
        private void CheckUserExists(string email)
        {
            List<string> emailsList = new List<string>();

            foreach (var user in PetOwnerList)
            {
                emailsList.Add(user.Email);
            }
            foreach (var user in PetSitterList)
            {
                emailsList.Add(user.Email);
            }

            foreach (var emails in emailsList)
            {
                if (emails != email)
                {
                    
                    foreach (var user in PetOwnerList)
                    {
                        if (user.Email == email) {
                            throw new UserAlreadyExistsException();
                        }
                    }
                    foreach (var user in PetSitterList)
                    {
                        if (user.Email == email) {
                            throw new UserAlreadyExistsException();
                        }
                    }
                    
                }
            }
        }   

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

    }
}