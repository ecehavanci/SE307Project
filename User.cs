using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    class User
    {
        protected String Name;
        protected String Surname;
        protected String Email;
        protected String Location;
        protected String Password;
        protected List<Message> MessageBox;

        public User()
        {
            MessageBox = new List<Message>();
        }

        public void EditProfile()
        {

        }

    }
}
