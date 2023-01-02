using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SE307Project
{
    public abstract class User
    {
        public User()
        {
        }

        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Location { get; set; }

        public String Password { get; set; }
        public List<Message> MessageBox { get; set; }
        public DateTime SignUpTime { get; set; }

        public User(string name, string surname, string email, string password)
        {
            MessageBox = new List<Message>();
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            SignUpTime = DateTime.Now;
        }
        
        //This method enables users to alter the data in their profiles. It works differently in PetOwner and PetSitter
        //but its aim is the same. So it is an abstract method.
        public abstract void EditProfile();

        //PetOwners and PetSitters can show profiles the same up to some point but PetSitters have bio and PetOwners
        //have pets differently. So this is a virtual method.
        public virtual void ShowProfile()
        {
            Console.WriteLine("********** " + Name + " " + Surname + "'s Profile **********");
            string profileInfo = "Location:\t" + (Location == null ? "N/A" : Location);
            Console.WriteLine(profileInfo);
        }


        public void AddMessage(Message message)
        {
            MessageBox.Add(message);
        }

        //Reading messages works different for PetSitter and PetOwner so this method is abstract.
        public abstract void ReadMessages();

        //Both type of users can change their password using the same method.
        public void ChangePassword()
        {
            while (true)
            {
                //User has to first enter the old password to change it
                Console.WriteLine("Please enter your current password, enter -1 to cancel password changing.");
                String oldPass = string.Empty;
                ConsoleKey key;
                
                //Passwords are never shown. Each character of passwords are change into stars while typing.
                do
                {
                    var keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && oldPass.Length > 0)
                    {
                        Console.Write("\b \b");
                        oldPass = oldPass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        oldPass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                
                //If user wants to cancel changing password
                if (oldPass == "-1")
                {
                    Console.WriteLine();
                    return;
                }
                
                //If entered password is wrong a warning is shown
                if (oldPass != Password)
                {
                    Console.WriteLine("\nWrong password");
                }
                else
                {
                    //If entered password is right this loop is broken so password changing can start
                    Console.WriteLine();
                    break;
                }
            }
            
            bool loop = true;
            while (loop)
            {
                //User enters new password
                Console.WriteLine("Write your new password");
                var newpass = string.Empty;
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && newpass.Length > 0)
                    {
                        Console.Write("\b \b");
                        newpass = newpass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        newpass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);


                //If the new password is suitable is checked
                if (newpass.Length >= 6 && Regex.IsMatch(newpass, @"\d") && Regex.IsMatch(newpass, @"[A-Z]"))
                {
                    //User enters new password again to verify
                    Console.WriteLine("\nPlease write your password again.");
                    var newpass2 = string.Empty;
                    ConsoleKey key2;
                    do
                    {
                        var keyInfo2 = Console.ReadKey(true);
                        key2 = keyInfo2.Key;

                        if (key2 == ConsoleKey.Backspace && newpass2.Length > 0)
                        {
                            Console.Write("\b \b");
                            newpass2 = newpass2[0..^1];
                        }
                        else if (!char.IsControl(keyInfo2.KeyChar))
                        {
                            Console.Write("*");
                            newpass2 += keyInfo2.KeyChar;
                        }
                    } while (key2 != ConsoleKey.Enter);

                    if (newpass2.Length >= 6 && Regex.IsMatch(newpass2, @"\d") && Regex.IsMatch(newpass, @"[A-Z]"))
                    {
                        if (newpass2 == newpass)
                        {
                            Password = newpass2;
                            Console.WriteLine("Password successfully changed.");
                            loop = false;
                        }
                        else
                        {
                            Console.WriteLine("\nPasswords doesn't match.");
                            continue;
                        }
                    }
                }

                Console.WriteLine(
                    "\nThe password must be at least 6 digits and contain at least one number and uppercase letter. Please try again.");
            }
        }


        //Show messages for a user given
        public void ShowMessagesFor(User messagesOfWhom)
        {
            bool showMyName = true;
            bool showOthersName = true;
            
            //Since messages are saved in the order they are sent they will be shown in ascending order by date
            foreach (var message in MessageBox)
            {
                //Show the message of a specific person
                if (message.SenderMail == messagesOfWhom.Email)
                {
                    //Show the name only once if there are multiple messages sent by the same user without other user answering
                    if (showOthersName)
                    {
                        Console.WriteLine(messagesOfWhom.Name + " " + messagesOfWhom.Surname + " (" + message.Date +
                                          "):");
                        showOthersName = false;
                        showMyName = true;
                    }

                    Console.WriteLine(message);
                }
                
                //Show my message
                if (message.SenderMail == Email && message.ReceiverMail == messagesOfWhom.Email)
                {
                    if (showMyName)
                    {
                        Console.WriteLine("You (" + message.Date + "):");
                        showMyName = false;
                        showOthersName = true;
                    }

                    Console.WriteLine(message);
                }
            }
        }


        public abstract String ToString();
    }
}