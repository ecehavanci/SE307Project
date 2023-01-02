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

            Database db = Database.GetInstance();//create the database instance

            XMLHandler xmlHandler = new XMLHandler();//create new xml handler

            db.PetOwnerList = xmlHandler.ReadPetOwnerList(db.XmlOwnerFileName);//read pet owner list from xml
            db.PetSitterList = xmlHandler.ReadPetSitterList(db.XmlSitterFileName);//read pet sitter list from xml


            while (true)
            {
                User user = LoginScreen(db); //login or sign up or exit screen
                if (user == null)
                {
                    continue;
                }

                Console.WriteLine("Welcome " + user.Name + "!"); //after login or sign up enter the system


                if (user is PetOwner)//if user is a pet owner
                {
                    PetOwner petOwner = (PetOwner) user;
                    bool isLoggedIn = true;
                    while (isLoggedIn) //if user is a pet owner list the options that user can do
                    {
                        Console.WriteLine("1) List pet sitters");
                        Console.WriteLine("2) View profile");
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
                            case 1://list pet sitters choice 
                                List<PetSitter> list = db.ListPetSitters(); 
                                Console.WriteLine("1) Send request\n2) Mark as hired\n3) Comment and rate\n4) Exit");
                                String actionChoice = Console.ReadLine();
                                switch (actionChoice)
                                {
                                    case "1": //send request from choosen pet sitter from pet sitter list
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
                                    case "2": //mark chosen pet sitter hired from pet sitter list
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
                                    case "3"://see or add comments and stars of choosen pet sitter from pet sitters list
                                        Console.WriteLine("Enter the index of the pet sitter you want to see comments of, enter -1 to exit.");
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
                                                    list[petSitterIndex - 1].ShowCommentsAndRates(); //show the comments
                                                    Console.WriteLine("Do you want to make comment? (Y/N)");
                                                    if (Console.ReadLine().ToUpper() == "Y")
                                                    {
                                                        petOwner.MakeCommentToPetSitter(list[petSitterIndex - 1]); //make a comment
                                                    }
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
                                
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList); //renew the pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList); //renew the pet owner list
                                break;
                            case 2:
                                user.ShowProfile();//show user profile
                                user.EditProfile();//edit profile page
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);//renew the pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew the pet owner list
                                
                                break;
                            case 3:
                                user.ReadMessages(); //read messages page
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);//renew the pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew the pet owner list
                                break;
                            case 4:
                                isLoggedIn = false; //log out
                                break;
                            case 5:
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);//renew the pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew the pet owner list
                                Environment.Exit(0); //shut down the system
                                break;
                        }
                    }
                }


                if (user is PetSitter)//if user is a pet sitter
                {
                    PetSitter petSitter = (PetSitter) user;
                    bool isLoggedIn = true;
                    while (isLoggedIn)
                    {
                        //show options in main page
                        Console.WriteLine("1) List request");
                        Console.WriteLine("2) View profile");
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
                            Console.WriteLine("Please enter a valid choice.");
                        }

                        switch (mainPageChoice)
                        {
                            case 1://list the received requests came from pet owners
                                petSitter.ReadRequests();

                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList); //renew pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew pet owner list
                                break;
                            case 2://view or edit the profile
                                user.ShowProfile(); 
                                
                                Console.WriteLine("\nWould you like to edit your profile? (Y/N)");

                                String editingChoice = Console.ReadLine();
                                if (editingChoice.ToUpper()=="Y")
                                {
                                    user.EditProfile();
                                    xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
                                }
                                
                                break;
                            case 3://read received messages/forwarded messages between logged in user and other user
                                user.ReadMessages();

                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);//renew pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew pet owner list
                                break;
                            case 4://log out 
                                isLoggedIn = false;
                                break;
                            case 5://shut down the system
                                xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);//renew pet sitter list
                                xmlHandler.WritePetOwnerList(db.XmlOwnerFileName, db.PetOwnerList);//renew pet owner list
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
            }
        }

        public static User LoginScreen(Database db) //handle sign in, sign up 
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
                    case "1": //sign up choice
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
                                //check if name fits name constraints with regex
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
                                //check if name fits name constraints with regex
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
                                //check if Email fits Email constraints with regex
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
                                newPassword = string.Empty;
                                
                                ConsoleKey key;
                                do
                                {//use to not show password on screen, every character is shown with (*)
                                    var keyInfo = Console.ReadKey(intercept: true);
                                    key = keyInfo.Key;

                                    if (key == ConsoleKey.Backspace && newPassword.Length > 0)
                                    {
                                        Console.Write("\b \b");
                                        newPassword = newPassword[0..^1];
                                    }
                                    else if (!char.IsControl(keyInfo.KeyChar))
                                    {
                                        Console.Write("*");
                                        newPassword += keyInfo.KeyChar;
                                    }
                                } while (key != ConsoleKey.Enter);
                                Console.WriteLine("\n");

                                if (newPassword.Length >= 6 && Regex.IsMatch(newPassword, @"\d") && Regex.IsMatch(newPassword, @"[A-Z]"))
                                {
                                    break;
                                }
                                Console.WriteLine("The password must be at least 6 digits and contain at least one number and uppercase letter. Please try again.");
                            }

                            switch (signUpChoice)
                            {
                                case 1:
                                    db.InsertPetSitter(newName, newSurname, newEmail, newPassword);//add pet sitter to xml pet sitter users
                                    break;
                                case 2:
                                    db.InsertPetOwner(newName, newSurname, newEmail, newPassword);//add pet owner to xml pet sitter owners
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Invalid choice.");

                        break;

                    case "2": //sign in choice
                        if (db.PetSitterList.Count == 0 && db.PetOwnerList.Count == 0) //check db has any users
                        {
                            Console.WriteLine("There is no account. Please create an account for logging in.");
                            continue;
                        }

                        Console.WriteLine("Email: ");
                        String email = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        String password = string.Empty;
                        ConsoleKey key2; //use to not show password on screen, every character is shown with (*)
                        do
                        {
                            var keyInfo = Console.ReadKey(intercept: true);
                            key2 = keyInfo.Key;

                            if (key2 == ConsoleKey.Backspace && password.Length > 0)
                            {
                                Console.Write("\b \b");
                                password = password[0..^1];
                            }
                            
                            else if (!char.IsControl(keyInfo.KeyChar))
                            {
                                Console.Write("*");
                                password += keyInfo.KeyChar;
                            }
                        } while (key2 != ConsoleKey.Enter);
                        Console.WriteLine("\n");
                        
                        try
                        {
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