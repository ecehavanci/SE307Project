using CsvHelper;
using CsvHelper.Configuration;

using System;
using System.Collections.Generic;

namespace SE307Project
{
    public abstract class User : ClassMap<User>
    {
        protected String Name { get; set; }
        protected String Surname { get; set; }
        protected String Email { get; set; }
        public String _Email { get {return Email;} }


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

        public abstract void EditProfile();

        public void ShowProfile()
        {
            Console.WriteLine("********** My Profile **********");
            string ProfileInfo = "Name:\t" + Name + "\nSurname:\t" + Surname + "\nEmail:\t" + Email + "\nLocation:\t" + Location;
            Console.WriteLine(ProfileInfo);
        }

        public void ReadMessages()
        {

        }
        
        public void AddMessage(Message message)
        {
            MessageBox.Add(message);
        }

        public void ChangePassword()
        {
            bool loop = true;
            Console.WriteLine("Write your new password");
            string pass = Console.ReadLine();
            Console.WriteLine("Please write your password again.");
            string pass2 = Console.ReadLine();
            while (true)
            {
                if (pass == pass2)
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

        public abstract void ShowMessagesFor(String email);


    }

}
