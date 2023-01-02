using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    //to catch the wrong entered password
    public class ExceptionWrongPassword : Exception
    {

        public ExceptionWrongPassword() : base("Entered password is wrong! Try Again.")
        {
        }

        public void PrintException()
        {
            Console.WriteLine(Message);
        }
    }

    //to catch the wrong entered email
    class ExceptionWrongEmail : Exception
    {
        private String Email;

        public ExceptionWrongEmail(string email) : base("Entered email does not exist! Try Again.")
        {
            Email = email;
        }

        public void PrintException()
        {
            Console.WriteLine(Message);
        }
    }
    //to catch if user already exists in the system database
    class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("Entered email is already exist. Please use new one.")
        {
        }

        public void PrintException()
        {
            Console.WriteLine(Message);
        }
    }

}
