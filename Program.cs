using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
            Database db = new Database("server=server_name;database=database_name;email=email;password=password");

            //var po = new PetOwner("Selina", "Kyle", "1", "1");
            //var ps = db.AddPetSitter("Gizem", "Kilic", "2", "2");
            //bool logOut = false;
            
            XMLHandler xmlHandler = new XMLHandler();
            //xmlHandler.WritePetOwnerList("bettertemp2.xml", db.);
            db.PetOwnerList = xmlHandler.ReadPetOwnerList(db.XmlOwnerFileName);
            db.PetSitterList = xmlHandler.ReadPetSitterList(db.XmlSitterFileName);
             

            while (true)
            {
                User user = LoginScreen(db);
                if (user == null)
                {
                    continue;
                }
                Console.WriteLine("Welcome " + user.Name);

                if (user is PetOwner)
                {
                    PetOwner petOwner = (PetOwner) user;
                    bool isLoggedIn = true;
                    while (isLoggedIn)
                    {
                        Console.WriteLine("1) List pet sitters");
                        Console.WriteLine("2) Edit profile");
                        Console.WriteLine("3) Read messages");
                        Console.WriteLine("4) Sign out");
                        Console.WriteLine("5) Terminate the system");


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
                            case 1:
                                List<PetSitter> list = db.ListPetSitters();
                                Console.WriteLine("Which one do you want to send a request? Enter -1 to exit.");
                                while (true)
                                {
                                    int petSitterIndex = int.Parse(Console.ReadLine());
                                    try
                                    {
                                        if (petSitterIndex == -1)
                                        {
                                            break;
                                        }

                                        if (petSitterIndex > list.Count || petSitterIndex < 0)
                                        {
                                            Console.WriteLine("Please enter a number between 0 and " + list.Count);
                                        }
                                        else
                                        {
                                            petOwner.SendRequestToPetSitter(list[petSitterIndex-1]);
                                            break;
                                        }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Please enter a number.");
                                        throw;
                                    }
                                }

                                break;
                            case 2:
                                user.EditProfile();
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 3:
                                user.ReadMessages();
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 4:
                                isLoggedIn = false;
                                break;
                            case 5:
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
                
                
                if (user is PetSitter)
                {
                    PetSitter petSitter = (PetSitter) user;
                    bool isLoggedIn = true;
                    while (isLoggedIn)
                    {
                        Console.WriteLine("1) List request");
                        Console.WriteLine("2) Edit profile");
                        Console.WriteLine("3) Read messages");
                        Console.WriteLine("4) Sign out");
                        Console.WriteLine("5) Terminate the system");


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
                            case 1:
                                petSitter.ReadRequestBox();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                break;
                            case 2:
                                user.EditProfile();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                break;
                            case 3:
                                user.ReadMessages();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                break;
                            case 4:
                                isLoggedIn = false;
                                break;
                            case 5:
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
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

                String choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
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

                            switch (signUpChoice)
                            {
                                case 1:
                                    db.InsertPetSitter(newName, newSurname, newEmail, newPassword);
                                    break;
                                case 2:
                                    db.InsertPetOwner(newName, newSurname, newEmail, newPassword);
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Invalid choice.");

                        break;

                    case "2":
                        if (db.PetSitterList.Count == 0 && db.PetOwnerList.Count == 0)
                        {
                            //TODO: When connection of sql is made, change it.
                            Console.WriteLine("There is no account. Please create an account for logging in.");
                            continue;
                        }

                        Console.WriteLine("Email: ");
                        String email = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        String password = Console.ReadLine();
                        try
                        {
                            //user = db.LogIn(email, password);
                            User temp = db.FindUser(email, password);
                            if (temp != null)
                            {
                                Console.WriteLine("Sign in successful!");
                                return temp;
                            }
                            else
                            {
                                Console.WriteLine("Email not found");
                            }

                            isSigned = true;
                            if (isSigned)
                            {
                                Console.WriteLine("Sign in successful!");
                            }
                        }
                        catch (ExceptionWrongEmail e)
                        {
                            e.PrintException();
                        }
                        catch (ExceptionWrongPassword e)
                        {
                            e.PrintException();
                        }

                        break;

                    case "3":
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

    
}