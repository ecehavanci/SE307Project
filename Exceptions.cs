using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    class WrongRequestSelection : Exception
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
    
    class ExceptionWrongPassword : Exception
    {

        public ExceptionWrongPassword() : base("Entered password is wrong! Try Again.")
        {
        }

        public void PrintException()
        {
            Console.WriteLine(Message);
        }
    }

}
