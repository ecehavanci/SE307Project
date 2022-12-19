using CsvHelper;
using CsvHelper.Configuration;

using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class User : ClassMap<User>
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
        protected DateTime signUpTime { get; set; }

        private RequestBox AcceptedRequestsBox { get; set; }
        private RequestBox RejectedRequestsBox { get; set; }
        private RequestBox WaitingRequestsBox { get; set; }

        public User(string name,string surname,string email,string password,string Location)
        {
            MessageBox = new List<Message>();
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Location = Location;
            this.Password = password;
            signUpTime = DateTime.Now;
            this.WaitingRequestsBox = new RequestBox(this,AcceptionEnum.Waiting);
            this.AcceptedRequestsBox = new RequestBox(this,AcceptionEnum.Accepted);
            this.RejectedRequestsBox = new RequestBox(this,AcceptionEnum.Rejected);

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
