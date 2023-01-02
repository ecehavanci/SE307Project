using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SE307Project
{
    public class PetSitter : User
    {
        public PetSitter()
        {
            Comments = new List<Comment>();
            RejectedRequestBox = new RequestBox(); 
            WaitingRequestBox = new RequestBox();
            AcceptedRequestBox = new RequestBox();
            Bio = "";
        }

        public List<Comment> Comments;


        public RequestBox WaitingRequestBox;

        public RequestBox AcceptedRequestBox;

        public RequestBox RejectedRequestBox;

        public String Bio;

        public PetSitter(string name, string surname, string email, string password) : base(name, surname, email,
            password)
        {
            Comments = new List<Comment>();
            WaitingRequestBox = new RequestBox(StatusEnum.Waiting);
            AcceptedRequestBox = new RequestBox(StatusEnum.Accepted);
            RejectedRequestBox = new RequestBox(StatusEnum.Rejected);
        }

        public void AddComment(Comment comment) //receive the comments from pet owners
        {
            Comments.Add(comment);
            XMLHandler xmlHandler = new XMLHandler();
            Database db = Database.GetInstance();
            xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
        }

        public void AddRequest(Request request)//receive the coming request from pet owner to waiting request box
        {
            WaitingRequestBox.ReceiveRequest(request);
            XMLHandler xmlHandler = new XMLHandler();
            Database db = Database.GetInstance();
            xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
        }

        public void ReadRequests() //read the request within desired request box
        {
            while (true)
            {
                Console.WriteLine(
                    "Which Request Box do you wish to read?\n1)Waiting Requests\n2)Accepted Requests\n3)Rejected Requests\n4)Exit");
                int selection = 4;
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a number between 1 and 4");
                    continue;
                }
                if (selection == 4)
                {
                    break;
                }

                RequestBox selectedRequestBox = SelectRequestBox(selection);
                if (selectedRequestBox is null)
                {
                    Console.WriteLine("Please enter a number between 1 and 4");
                    continue;
                }
                
                selectedRequestBox.DisplayRequestBox();

                if (selectedRequestBox.IsEmpty())
                {
                    continue;
                }
                Console.WriteLine("Want to sort by requests according to Date? Y/N");
                String willSort = Console.ReadLine();
                if (willSort.Equals("Y"))
                {
                    Console.WriteLine("How would you like to sort? ASC(in ascending)/DESC(in Descending) Date");
                    String sortStyle = Console.ReadLine();
                    if (sortStyle == "ASC")
                    {
                        selectedRequestBox.SortByDateAsc();
                        selectedRequestBox.DisplayRequestBox();
                    }
                    else if (sortStyle.Equals("DESC"))
                    {
                        selectedRequestBox.SortByDateDesc();
                        selectedRequestBox.DisplayRequestBox();
                    }
                    else
                    {
                        Console.WriteLine("Sorting is available only ascending or descending, aborting action...");
                    }
                }
                else if (willSort.Equals("N"))
                    selectedRequestBox.DisplayRequestBox();
                else
                {
                    Console.WriteLine("You wrote incorrect input.");
                    selectedRequestBox.DisplayRequestBox();
                }

                if (!selectedRequestBox.IsEmpty())
                {
                    Console.WriteLine("Would you like to 1)Read / 2)Accept/ 3)Reject any Request? 4)Exit");
                    String requestToDo = Console.ReadLine();
                    if (requestToDo == "1")
                    {
                        try
                        {
                            Console.WriteLine("Which request do you want to read? Choose below:");
                            selectedRequestBox.DisplayRequestBox();
                            int selectedRequest = Convert.ToInt32(Console.ReadLine());
                            selectedRequestBox.ReadRequest(selectedRequest-1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        
                    }
                    else if (requestToDo == "2")
                    {
                        Console.WriteLine("Which Request Do you want to accept? Choose below:");
                        selectedRequestBox.DisplayRequestBox();
                        int selectedRequest = Convert.ToInt32(Console.ReadLine());
                        AcceptRequest(selectedRequestBox.MoveMailToAnotherBox(selectedRequest - 1));
                    }
                    else if (requestToDo == "3")
                    {
                        Console.WriteLine("Which Request Do you want to reject? Choose below:");
                        selectedRequestBox.DisplayRequestBox();
                        int selectedRequest = Convert.ToInt32(Console.ReadLine());
                        RejectRequest(selectedRequestBox.MoveMailToAnotherBox(selectedRequest - 1));

                    }
                    else if (requestToDo == "4")
                    {
                        break;
                    }
                }
                
            }
        }

        private RequestBox SelectRequestBox(int BoxChoice)//select the desired request box
        {
            switch (BoxChoice)
            {
                case 1:
                    return WaitingRequestBox;
                case 2:
                    return
                        AcceptedRequestBox;
                case 3:
                    return RejectedRequestBox;
                default:
                    return null;
            }
        }

        public void AcceptRequest(Request request)//accept the request from any request box
        {
            Database db = Database.GetInstance();
            try
            {
                PetOwner po = db.FindPetOwner(request.RequestOwnerEmail);
                po.PetSitterContacts.Add((this, false));
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Request owner not found. Cannot accept request");
                return;
            }

            AcceptedRequestBox.ReceiveRequest(request);
            Console.WriteLine("Request Accepted!");
        }

        public void RejectRequest(Request request)//reject the request from any request box
        {
            RejectedRequestBox.ReceiveRequest(request);
            Console.WriteLine("Request Rejected!");
        }

        public override void ShowProfile()//show pet sitter profile
        {
            base.ShowProfile();
            Console.WriteLine(ToString());
        }

        public void SendHiringRejectedMessage(PetOwner petOwner)//send a rejected message to pet owner when pet sitter rejected a hiring
        {
            petOwner.AddMessage(new HiringMessage(petOwner.Email, Email, 2));
        }

        public override void ReadMessages() //read messages
        {
            Database db = Database.GetInstance();

            if (MessageBox.Count == 0)
            {
                Console.WriteLine("You have no messages yet.");
                return;
            }

            Dictionary<int, PetOwner> poIndexes = new Dictionary<int, PetOwner>();

            Console.WriteLine("You are in contact with:");
            int index = 0;

            bool isFirstHiringMessage = true;
            HiringMessage hiringMessage = null;
            for (int i = 0; i < MessageBox.Count; i++)
            {
                if (MessageBox[i].SenderMail == "system")//if the message is sent by system it means message is for hiring
                {
                    if (isFirstHiringMessage)//if pet owner send a hiring message there is a text to show
                    {
                        hiringMessage = (HiringMessage) MessageBox[i];
                        Console.WriteLine("There are people who claims that they hired you. Letting them marking you " +
                                          "as hired let them comment and rate you.");
                        isFirstHiringMessage = false;
                    }

                    Console.WriteLine(MessageBox[i]);
                    String hiredInput = Console.ReadLine();//"Y" if hired
                    PetOwner po = (PetOwner) db.FindUser(hiringMessage.RelatedEmail);//find pet owner to accept or reject hiring
                    
                    if (hiredInput.ToUpper() == "Y")//accept the hiring message
                    {
                        if (po != null)
                        {
                            po.HirePetSitter(this);
                            MessageBox.RemoveAt(i);
                            Console.WriteLine("Request accepted.");
                        }
                       
                    }
                    else //reject the hiring message
                    {
                        SendHiringRejectedMessage(po);
                        Console.WriteLine("Request rejected.");
                        MessageBox.RemoveAt(i);
                    }
                }
            }

            //This is for obtaining message owners
            foreach (var message in MessageBox)
            {
                try
                {
                    PetOwner
                        po = db.FindPetOwner(message
                            .SenderMail); //There has to be at least one message sent by pet owner
                    if(po is null)
                    {
                        continue;
                    }
                    if (!poIndexes.ContainsValue(po))
                    {
                        poIndexes.Add(index, po);
                        Console.WriteLine(index + 1 + ") " + po.Name + " " + po.Surname);
                        index++;
                    }
                }
                catch (NullReferenceException e)
                {
                    continue;
                }
            }

            Console.WriteLine("Enter the index of the pet owner you want to see messages of, enter -1 to exit");
            PetOwner messagesOfWhom = null;
            while (true)
            {
                try
                {
                    int messagesOfWhomIndex = int.Parse(Console.ReadLine());
                    if (messagesOfWhomIndex == -1)
                    {
                        return;
                    }

                    messagesOfWhom = poIndexes[messagesOfWhomIndex - 1];
                    break;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter the index of whose messages you want to read or -1 to exit");
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Please enter a number between 1-" + poIndexes.Count + " or -1");
                }
            }

            ShowMessagesFor(messagesOfWhom);
            Console.WriteLine("Do you want to send message to " + messagesOfWhom.Name + "? (Y/N)");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                SendMessageToPetOwner(messagesOfWhom);
            }
        }

        public override void EditProfile()//edit the profile
        {
            Console.WriteLine(Bio == "" ? Bio : "No Biography available.");

            int selection = -1;
            while (selection != 7)
            {
                String location = String.IsNullOrEmpty(Location) ? "Unknown" : Location;
                Console.WriteLine(
                    "Which part of your profile you want to edit? \n1) Name (" + Name + ")\n2) Surname (" + Surname +
                    ")\n3) Email (" + Email + ")\n4) Password\n5) Location (" + location + ") \n6) Bio\n7) Save & Exit");

                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a number 1-7");
                    continue;
                }

                switch (selection)
                {
                    case 1:
                        string newName;
                        while (true)
                        {
                            Console.WriteLine("New name: ");
                            newName = Console.ReadLine();
                            //match if input is in the format of an name
                            if (newName != "" && Regex.IsMatch(newName, @"^[a-zA-Z]*$") && !Regex.IsMatch(newName, @"\d"))
                            {
                                Name = newName;
                                break;
                            }
                            Console.WriteLine("Name is not valid. Please try again.");
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        string newSurname;
                        while (true)
                        {
                            Console.Write("New surname: ");
                            newSurname = Console.ReadLine();
                            //match if input is in the format of an name
                            if (newSurname != "" && Regex.IsMatch(newSurname, @"^[a-zA-Z]*$") && !Regex.IsMatch(newSurname, @"\d"))
                            {
                                Surname = newSurname;
                                break;
                            }
                            Console.WriteLine("Surname is not valid. Please try again.");
                        }
                        Console.WriteLine();
                        break;

                    case 3:
                        string newEmail;
                        while (true)
                        {
                            Console.WriteLine("New Email: ");
                            newEmail = Console.ReadLine();
                                //match if input is in the format of an mail
                            if (Regex.IsMatch(newEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                            {
                                Database db = Database.GetInstance();
                                User u = db.FindUser(newEmail);
                                if (u == null)
                                {
                                    Email = newEmail;
                                    break;
                                }

                                if (u.Email == Email)
                                {
                                    Console.WriteLine(
                                        "Entered email is your current email. Do you wish to exit without changing? (Y/N)");
                                    if (Console.ReadLine().ToUpper() == "Y")
                                    {
                                        break;
                                    }

                                    continue;
                                }

                                Console.WriteLine("There is another user with same email. Please try again.");
                                continue;
                            }
                            Console.WriteLine("Email is not valid. Please try again.");
                        }
                       
                        Console.WriteLine();
                        break;
                    case 4:
                        ChangePassword();
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.Write("New location: ");
                        string newLocation = Console.ReadLine();
                        Location = newLocation;
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine("New biography: ");
                        String biog = Console.ReadLine();
                        Bio = biog;
                        break;
                }
            }
        }

        public void ShowCommentsAndRates()//show both comments and rates(stars)
        {
            Console.WriteLine("-----------------------");
            if (Comments.Count == 0)
            {
                Console.WriteLine("\nNo comments yet\n");
            }
            else
            {
                Console.WriteLine("\nMy Comments & Rates");
                foreach (Comment comment in Comments)
                {
                    Console.WriteLine("********");
                    Console.WriteLine(comment.ToString());
                    Console.WriteLine("********");
                }
            }
            Console.WriteLine("-----------------------");
            
        }

  

        public void SendMessageToPetOwner(PetOwner po)//send message to a pet owner
        {
            Console.WriteLine("Message:");
            String messageText = Console.ReadLine();
            Message message = new Message(Email, po.Email, messageText);
            po.AddMessage(message);
            AddMessage(message);
        }


        public double CalculateAverageStars()//calculate the average of stars from list of comment's stars
        {
            double counter = 0;
            for (int i = 0; i < Comments.Count; i++)
            {
                counter += Comments[i].Star;
            }

            return counter / Comments.Count;
        }

        public override String ToString()//print out both average rate and bio
        {
            String rate = Comments.Count == 0
                ? "- no rate yet -"
                : (CalculateAverageStars() + "/5");
            return "Bio:\n" + Bio + "\nRate: " + rate;
        }
    }
}