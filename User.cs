using CsvHelper;
using CsvHelper.Configuration;

using System;
using System.Collections.Generic;

namespace SE307Project
{
    public abstract class User : ClassMap<User>
    {
        protected String Name { get; set; }
        public String _Name
        {
            get { return Name; }
            set { Name = _Name; }
        }
        protected String Surname { get; set; }
        public String _Surname
        {
            get { return Surname; }
            set { Surname = _Surname; }
        }
        protected String Email { get; set; }
        public String _Email
        {
            get { return Email; }
            set { Email = _Email; }
        }

        protected String Location { get; set; }
        public String _Location
        {
            get { return Location; }
            set { Location = _Location; }
        }
        protected String Password;
        
        public String _Password
        {
            get { return Password; }
            set { Password = _Password; }
        }
        
        protected List<Message> MessageBox { get; set; }
        public List<Message> _MessageBox
        {
            get { return MessageBox; }
            set { MessageBox = _MessageBox; }
        }
        protected DateTime SignUpTime { get; set; }
        
        public User(): this("","","","")
        {
        }

        public User(string name,string surname,string email,string password)
        {
            MessageBox = new List<Message>();
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            SignUpTime = DateTime.Now;

            Map(p => p.Email).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.Surname).Index(2);
            Map(p => p.Password).Index(3);
            Map(p => p.Location).Index(4);
            Map(p => p.SignUpTime).Index(5);

        }

        public abstract void EditProfile();

        public void ShowProfile()
        {
            Console.WriteLine("********** My Profile **********");
            string profileInfo = "Name:\t" + Name + " " + Surname + "\nLocation:\t" + (Location == null? "N/A" : Location);
            Console.WriteLine(profileInfo);
        }
        
        public void AddMessage(Message message)
        {
            MessageBox.Add(message);
        }

        public void ChangePassword()
        {
            bool loop = true;
            Console.WriteLine("Write your new password");
            var newpass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && newpass.Length > 0)
                {
                    Console.Write("\b \b");
                    newpass = newpass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    newpass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine("Please write your password again.");
            var newpass2 = string.Empty;
            ConsoleKey key2;
            do
            {
                var keyInfo2 = Console.ReadKey(intercept: true);
                key2 = keyInfo2.Key;

                if (key2 == ConsoleKey.Backspace && newpass2.Length > 0)
                {
                    Console.Write("\b \b");
                    newpass2 = newpass2[0..^1];
                }
                else if (!char.IsControl(keyInfo2.KeyChar))
                {
                    Console.Write("*");
                    newpass += keyInfo2.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            while (true)
            {
                if (newpass2 == newpass)
                {
                    Password = newpass2;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Passwords doesn't match.");
                }
            }
        }

        public abstract void ShowMessagesFor(String email);
        
        public abstract String ToString();


    }

}
