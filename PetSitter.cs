using System;
using System.Collections.Generic;
using System.Data.Common;
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

        //public List<PetOwner> PetOwnerContacts;

        public RequestBox WaitingRequestBox;

        public RequestBox AcceptedRequestBox;

        public RequestBox RejectedRequestBox;

        public String Bio;

        public PetSitter(string name, string surname, string email, string password) : base(name, surname, email,
            password)
        {
            Comments = new List<Comment>();
            //PetOwnerContacts = new List<PetOwner>();
            WaitingRequestBox = new RequestBox(StatusEnum.Waiting);
            AcceptedRequestBox = new RequestBox(StatusEnum.Accepted);
            RejectedRequestBox = new RequestBox(StatusEnum.Rejected);
        }
        
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
            XMLHandler xmlHandler = new XMLHandler();
            Database db = Database.GetInstance();
            xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
        }
        
        public void AddRequest(Request request)
        {
            WaitingRequestBox.ReceiveRequest(request);
            XMLHandler xmlHandler = new XMLHandler();
            Database db = Database.GetInstance();
            xmlHandler.WritePetSitterList(db.XmlSitterFileName, db.PetSitterList);
        }

        public void ReadRequestBox()
        {
            //bool loop = true;

            while (/*loop*/true)
            {
                Console.WriteLine(
                    "Which Request Box do you wish to read?\n1)Waiting Requests\n2)AcceptedRequests\n3)Rejected Requests\n4)Exit");
                int selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 4)
                {
                    //loop = false;
                    break;
                }
                RequestBox selectedRequestBox = SelectRequestBox(selection);
                if (selectedRequestBox is null)
                {
                    Console.WriteLine("Incorrect input.");
                    continue;
                }

                selectedRequestBox.DisplayRequestBox();
                Console.WriteLine("Want to sort by requests according to Date? Y/N");
                string willSort = Console.ReadLine();
                if (willSort.Equals("Y"))
                {
                    Console.WriteLine("How would you like to sort? ASC(in ascending)/DESC(in Descending) Date");
                    string sortStyle = Console.ReadLine();
                    if (sortStyle.Equals("ASC"))
                    {
                        selectedRequestBox.SortByDateAsc();
                    }
                    else if (sortStyle.Equals("DESC"))
                    {
                        selectedRequestBox.SortByDateDesc();
                    }
                }
                else if (willSort.Equals("N"))
                    selectedRequestBox.DisplayRequestBox();

                Console.WriteLine("Would you like to 1)Read / 2)Acccept/ 3)Reject any Request?");
                String requestToDo = Console.ReadLine();
                if (requestToDo == "1")
                {
                    //TODO Read Request
                }
                else if (requestToDo == "2")
                {
                    Console.WriteLine("Which Request Do you want to Accept? Choose below:");
                    selectedRequestBox.DisplayRequestBox();
                    int selectedRequest = Convert.ToInt32(Console.ReadLine());
                    AcceptRequest(selectedRequestBox.MoveMailToAnotherBox(selectedRequest - 1));
                }
                else if (requestToDo == "3")
                {
                    Console.WriteLine("Which Request Do you want to Reject? Choose below:");
                    selectedRequestBox.DisplayRequestBox();
                    int selectedRequest = Convert.ToInt32(Console.ReadLine());
                    RejectRequest(selectedRequestBox.MoveMailToAnotherBox(selectedRequest - 1));

                }
            }

        }

        private RequestBox SelectRequestBox(int BoxChoice)
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

        public void AcceptRequest(Request request)
        {
            AcceptedRequestBox.ReceiveRequest(request);
            Console.WriteLine("Request Accepted!");
        }

        public void RejectRequest(Request request)
        {
            RejectedRequestBox.ReceiveRequest(request);
            Console.WriteLine("Request Rejected!");
        }

        public override void ShowProfile()
        {
            base.ShowProfile();
            Console.WriteLine("Bio:\n" + Bio);
        }

        public override void ReadMessages()
        {
            
        }

        public override void EditProfile()
        {
            //ShowProfile();
            Console.WriteLine(Bio == "" ? Bio : "No Biography available.");
            String location = String.IsNullOrEmpty(Location) ? "Unknown" : Location;

            int selection = -1;
            while (selection != 7)
            {
                Console.WriteLine(
                    "Which part of your profile you want to edit? \n1) Name(" + Name + ")\n2) Surname(" + Surname +
                    ")\n3) Email(" + Email + ")\n4) Password\n5) Location(" + location + ") \n6) Bio\n7) Exit");

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
                        Console.Write("New name: ");
                        string newName = Console.ReadLine();
                        Name = newName;
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.Write("New surname: ");
                        string newSurname = Console.ReadLine();
                        Surname = newSurname;
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.Write("New email: ");
                        string newMail = Console.ReadLine();
                        Email = newMail;
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Write("New password: ");
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
        public void ShowCommentsAndRates()
        {
            foreach (Comment comment in Comments)
            {
                comment.ToString();
            }
        }

        public override void ShowMessagesFor(String email)
        {
            bool isThereAnyMessage = false;
            foreach (var message in MessageBox)
            {
                if (message.ReceiverMail == email)
                {
                    Console.WriteLine(message.ToString());
                    isThereAnyMessage = true;

                }
            }

            if (!isThereAnyMessage)
            {
                Console.WriteLine("There are no messages.");

            }
            /*PetOwner petOwner = FindPetOwner(email);
            if(petOwner != null)
            {
                Console.WriteLine("Messages:\n");
                foreach (var message in MessageBox)
                {
                    Console.WriteLine(message.ToString());
                }
            }
            else
            {
                Console.WriteLine("No connection with email " + email);
            }*/
        }

        public void SendMessageToPetOwner(String o_email)
        {
            Database db = Database.GetInstance();
            PetOwner po = db.FindPetOwner(o_email);

            foreach (var contact in po.PetSitterContacts)
            {
                if (contact.Item1.Email == Email)
                {
                    Console.WriteLine("Message:");
                    String messageText = Console.ReadLine();
                    Message message = new Message(Email, contact.Item1.Email,messageText);
                    contact.Item1.AddMessage(message);
                    AddMessage(message);
                }
            }
           
        }

        /*private PetOwner FindPetOwner(String email)
        {
            foreach(PetOwner petOwner in PetOw)
            {
                if(email == petOwner.Email)
                {
                    return petOwner;
                }
            }
            return null;
        }*/
        private static double GetMedian(int[] sourceNumbers)
        {
            //Framework 2.0 version of this method. there is an easier way in F4        
            if (sourceNumbers == null || sourceNumbers.Length == 0)
                throw new System.Exception("Median of empty array not defined.");

            double[] sortedPNumbers = (double[]) sourceNumbers.Clone();
            Array.Sort(sortedPNumbers);

            //get the median
            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0)
                ? (double) sortedPNumbers[mid]
                : ((double) sortedPNumbers[mid] + (double) sortedPNumbers[mid - 1]) / 2;
            return median;
        }

        public double CalculateMedianStars()
        {
            int[] allRates = { };
            //Console.WriteLine("My Calculated Rate Median is:");
            for (int i = 0; i < Comments.Count; i++)
            {
                allRates[i] = Comments[i].Star;
            }

            return GetMedian(allRates);
        }

        public double CalculateAverageStars()
        {
            double counter = 0;
            Console.WriteLine("My Calculated Rate Average is:");
            for (int i = 0; i < Comments.Count; i++)
            {
                counter += Comments[i].Star;
            }

            return counter / Comments.Count;
        }

        public override String ToString()
        {
            return Name + " " + Surname + "\n" + Bio;
        }
    }
}