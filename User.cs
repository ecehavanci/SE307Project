using CsvHelper;
using CsvHelper.Configuration;

using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class User : ClassMap<User>
    {
        protected String Name { get; set; }
        protected String Surname { get; set; }
        protected String Email { get; set; }

        protected String Location { get; set; }
        protected String Password;
        protected List<Message> MessageBox { get; set; }
        protected DateTime signUpTime { get; set; }


        public User(string name,string surname,string email,string password,string Location)
        {
            MessageBox = new List<Message>();
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Location = Location;
            this.Password = password;
            signUpTime = DateTime.Now;

            Map(p => p.Email).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.Surname).Index(2);
            Map(p => p.Password).Index(3);
            Map(p => p.Location).Index(4);
            Map(p => p.signUpTime).Index(5);

        }

        public void EditProfile()
        {   
            Console.WriteLine("Which part of your profile you want to edit? \n1)Name \n2)Surname \nEmail \nLocation");
            int editWhere = Int32.Parse(Console.ReadLine());
            switch (editWhere) {
            case 1:
                    Console.WriteLine("Write new input for name");
                    string newName = Console.ReadLine();
                    this.Name = newName;
                    break;
                case 2:
                    Console.WriteLine("Write new input for Surname");
                    string newSurname = Console.ReadLine();
                    this.Name = newSurname;
                    break;
                case 3:
                    Console.WriteLine("Write new input for Email");
                    string newMail = Console.ReadLine();
                    this.Name = newMail;
                    break;
                case 4:
                    Console.WriteLine("Write new input for Location");
                    string newLoc = Console.ReadLine();
                    this.Name = newLoc;
                    break;
            }

        }

        public void ShowProfile()
        {
            Console.WriteLine("********** My Profile **********");
            string ProfileInfo = "Name:\t" + Name + "\nSurname:\t" + Surname + "\nEmail:\t" + Email + "\nLocation:\t" + Location;
            Console.WriteLine(ProfileInfo);
        }

        public void ReadMessages()
        {

        }

        public void ChangePassword()
        {
            bool loop = true;
            Console.WriteLine("Write your new password");
            string pass = Console.ReadLine();
            Console.WriteLine("Please write your password again.");
            string pass2 = Console.ReadLine();
            while (true) { 
                if(pass == pass2)
                {
                    Password = pass;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Passwords doesn't match.");
                 }
            }
            
        }

    }

}
