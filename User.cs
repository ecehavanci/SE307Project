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

        private RequestBox AcceptedRequestsBox { get; set; }
        private RequestBox RejectedRequestsBox { get; set; }
        private RequestBox WaitingRequestsBox { get; set; }

        public User(string name,string surname,string email,string password)
        {
            MessageBox = new List<Message>();
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            SignUpTime = DateTime.Now;
            WaitingRequestsBox = new RequestBox(this,AcceptionEnum.Waiting);
            AcceptedRequestsBox = new RequestBox(this,AcceptionEnum.Accepted);
            RejectedRequestsBox = new RequestBox(this,AcceptionEnum.Rejected);

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
        
        public abstract String ToString();


    }

}
