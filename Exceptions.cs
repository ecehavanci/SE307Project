using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public class WrongRequestSelection : Exception
    {
        public WrongRequestSelection() : base("Please enter correct selection number.") {

        }
        
        public void PrintException()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(Message);
            Console.ResetColor();
        }

    }
    
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
