using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*PetSitter sitter = new PetSitter("name","surname","email","pass","Location");
            FileHandler hander = new FileHandler();
            var users = new List<User>();
            users.Add(sitter);
            hander.saveToCSV(); */
            Database db = new Database();

            bool logOut = false;

            while (!logOut)
            {
                User user = LoginScreen(db);
                Console.WriteLine("Welcome " + user._Name);
            }
        }

        public static User LoginScreen(Database db)
        {
            User user = null;

            while (true)
            {
                bool isSigned = false;

                Console.WriteLine("Welcome!");
                Console.WriteLine("1) Sign up");
                Console.WriteLine("2) Sign in");
                Console.WriteLine("3) Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("What do you want to sign up as?");
                        Console.WriteLine("1) Pet Sitter");
                        Console.WriteLine("2) Pet Owner");
                        Console.WriteLine("3) Cancel registration");

                        int signUpChoice = Convert.ToInt32(Console.ReadLine());

                        if (signUpChoice == 3)
                            break;
                        if (signUpChoice == 1 || signUpChoice == 2)
                        {
                            Console.WriteLine("Name: ");
                            String newName = Console.ReadLine();
                            Console.WriteLine("Surname: ");
                            String newSurname = Console.ReadLine();
                            Console.WriteLine("Email: ");
                            String newEmail = Console.ReadLine();
                            Console.WriteLine("Password: ");
                            String newPassword = Console.ReadLine();
                            Console.WriteLine("Location: ");
                            String newLocation = Console.ReadLine();

                            //TODO: When connection of sql is made
                            switch (signUpChoice)
                            {
                                case 1:
                                    db.RegisterPetSitter(newName, newSurname, newEmail, newPassword,
                                        newLocation); //TODO:for sign up as a pet sitter
                                    break;
                                case 2:
                                    db.RegisterPetOwner(newName, newSurname, newEmail, newPassword,
                                        newLocation); //TODO:for sign up as a pet owner
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Invalid choice.");

                        break;

                    case 2:
                        if (db.UserList.Count == 0)
                        {
                            //TODO: When connection of sql is made, change it.
                            Console.WriteLine("There is no such account. Please create an account for logging in.");
                            continue;
                        }

                        Console.WriteLine("Email: ");
                        String email = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        String password = Console.ReadLine();
                        try
                        {
                            user = db.LogIn(email, password);
                            isSigned = true;
                        }
                        catch (ExceptionWrongPassword e)
                        {
                            e.PrintException();
                        }

                        if (user is PetOwner)
                        {
                            while (true)
                            {
                                Console.WriteLine("1) List pet sitters");
                                Console.WriteLine("2) Edit profile");
                                Console.WriteLine("3) Read messages");

                                int mainPageChoice = -1;
                                try
                                {
                                    mainPageChoice = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine("Please enter a valid choice");
                                }

                                switch (mainPageChoice)
                                {
                                    case 1 : 
                                        db.ListUsers();
                                        break;
                                    case 2 : 
                                        user.EditProfile();
                                        break;
                                    case 3 : 
                                        user.ReadMessages();
                                        break;
                                }
                            }
                        }

                        break;

                    case 3:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (isSigned)
                    return user;
            }
        }
    }

    class ExceptionWrongPassword : Exception
    {
        private String Password;

        public ExceptionWrongPassword(string password) : base("Entered password is wrong! Try Again.")
        {
            Password = password;
        }

        public void PrintException()
        {
            Console.WriteLine(Message);
        }
    }
}