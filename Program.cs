using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            Database db = Database.GetInstance();

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

                Console.WriteLine("Welcome " + user.Name + "!");

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
                                Console.WriteLine("1) Send request\n2) Mark as hired\n3) Comment and rate\n4) Exit");
                                String actionChoice = Console.ReadLine();
                                switch (actionChoice)
                                {
                                    case "1":
                                        Console.WriteLine("Which pet sitter do you want to send a request? Enter -1 to exit.");
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
                                                    Console.WriteLine("Please enter a number between 0 and " +
                                                                      list.Count);
                                                }
                                                else
                                                {
                                                    petOwner.SendRequestToPetSitter(list[petSitterIndex - 1]);
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
                                    case "2":
                                        Console.WriteLine("Which pet sitter do you want to mark as hired? Enter -1 to exit.");
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
                                                    Console.WriteLine("Please enter a number between 0 and " +
                                                                      list.Count);
                                                }
                                                else
                                                {
                                                    petOwner.SendHiringMessage(list[petSitterIndex - 1]);
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
                                    case "3":
                                        Console.WriteLine("Which pet sitter do you want to comment and rate? Enter -1 to exit.");
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
                                                    Console.WriteLine("Please enter a number between 0 and " +
                                                                      list.Count);
                                                }
                                                else
                                                {
                                                    petOwner.MakeCommentToPetSitter(list[petSitterIndex - 1]);
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
                                }


                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 2:
                                user.EditProfile();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 3:
                                user.ReadMessages();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
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
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 2:
                                user.EditProfile();
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                break;
                            case 3:
                                user.ReadMessages();

                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
                                break;
                            case 4:
                                isLoggedIn = false;
                                break;
                            case 5:
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                //xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);
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
                           
                            String newName;
                            while (true)
                            {
                                Console.WriteLine("Name: ");
                                newName = Console.ReadLine();

                                if (newName != "" && Regex.IsMatch(newName, @"^[a-zA-Z]*$") && !Regex.IsMatch(newName, @"\d"))
                                {
                                    break;
                                }
                                Console.WriteLine("Name is not valid. Please try again.");
                            }


                            String newSurname;
                            while (true)
                            {
                                Console.WriteLine("Surname: ");
                                newSurname = Console.ReadLine();

                                if (newSurname != "" && Regex.IsMatch(newSurname, @"^[a-zA-Z]*$") && !Regex.IsMatch(newSurname, @"\d"))
                                {
                                    break;
                                }
                                Console.WriteLine("Surname is not valid. Please try again.");
                            }
                            
                            String newEmail;
                            while (true)
                            {
                                Console.WriteLine("Email: ");
                                newEmail = Console.ReadLine();
                                
                                if (Regex.IsMatch(newEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                                {
                                    break;
                                }
                                Console.WriteLine("Email is not valid. Please try again.");
                            }
                            
                           
                            String newPassword;
                            while (true)
                            {
                                Console.WriteLine("Password: ");
                                newPassword = Console.ReadLine();

                                if (newPassword.Length >= 6 && Regex.IsMatch(newPassword, @"\d") && Regex.IsMatch(newPassword, @"[A-Z]"))
                                {
                                    break;
                                }
                                Console.WriteLine("The password must be at least 6 digits and contain at least one number and uppercase letter. Please try again.");
                            }

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
                            Console.WriteLine("Email not found");

                            isSigned = true;
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