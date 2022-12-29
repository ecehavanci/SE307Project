using System;
using System.Collections.Generic;

namespace SE307Project
{
    public interface ICalculable
    {
        public void CalculateMedian();
        public void CalculateAverage();

    }
    
    public class PetSitter : User
    {
        private List<Comment> Comments;
        private List<PetOwner> PetOwnerContacts;//???
        private RequestBox WaitingRequestBox;
        private RequestBox AcceptedRequestBox;
        private RequestBox RejectedRequestBox;
        private String Bio;

        public PetSitter(string name, string surname, string email, string password):base(name,surname,email,password)
        {
            Comments = new List<Comment>();
            PetOwnerContacts = new List<PetOwner>();
            WaitingRequestBox = new RequestBox(this,AcceptionEnum.Waiting);
            AcceptedRequestBox = new RequestBox(this,AcceptionEnum.Accepted);
            RejectedRequestBox = new RequestBox(this,AcceptionEnum.Rejected);
        }

        public PetSitter()
        {
        }
        
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
        
        public void AddRequest(Request request)
        {
            WaitingRequestBox.ReceiveRequest(request);
        }

        public void ReadRequestBox()
        {
            bool loop = true;

            while (loop) { 
                     Console.WriteLine("Which Request Box do you wish to read?\n1)Waiting Requests\n2)AcceptedRequests\n3)Rejected Requests\n4)Exit");
                     int selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 4)
                {
                    loop = false;
                    break;
                }
                RequestBox selectedRequestBox = SelectRequestBox(selection);
                    if (selectedRequestBox is null)
                    {
                        Console.WriteLine("Incorrect input.");
                        return;
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
                int requestToDo = Convert.ToInt32(Console.ReadLine());
                if (requestToDo ==1)
                {
                    //TODO Read Request
                }
                else if(requestToDo == 2)
                {
                    Console.WriteLine("Which Request Do you want to Accept? Choose below:");
                    selectedRequestBox.DisplayRequestBox();
                    int selectedRequest = Convert.ToInt32(Console.ReadLine());
                    AcceptRequest(selectedRequestBox.MoveMailToAnotherBox(selectedRequest-1));
                }
                else if(requestToDo == 3)
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

        public override void EditProfile()
        {
            this.ShowProfile();
            Console.WriteLine(this.Bio == ""?Bio:"No Biography available.");
            Console.WriteLine("Enter index of the are would you like to Edit");
            Console.WriteLine("1)Email\t2)Location\t3)Password\t4)Biography");
            int selection = Convert.ToInt32(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    Console.WriteLine("Enter the new Email:");
                    string newMail = Console.ReadLine();
                    this.Email = newMail;
                    break;
                case 2:
                    Console.WriteLine("Enter the new Location:");
                    string newLocation = Console.ReadLine();
                    this.Location = newLocation;
                    break;
                case 3:
                    Console.WriteLine("Enter the new Password:");
                    this.ChangePassword();
                    break;

                case 4:
                    Console.WriteLine("Edit your Biography:");
                    string biog = Console.ReadLine();
                    this.Bio = biog;
                    break;
            }

        }
        public void ShowCommentsAndRates()
        {
            foreach(Comment comment in Comments)
            {
                comment.ToString();
            }
        }

        public override void ShowMessagesFor(String email)
        {
            PetOwner petOwner = FindPetOwner(email);
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
            }
        }

        public void SendMessageToPetOwner(PetOwner petOwner)
        {
            if (PetOwnerContacts.Contains(petOwner))
            {
                Console.WriteLine("Message:");
                String messageText = Console.ReadLine();
                Message message = new Message(Email, petOwner._Email, messageText);
                petOwner.AddMessage(message);
                AddMessage(message);
            }
        }

        private PetOwner FindPetOwner(String email)
        {
            foreach(PetOwner petOwner in PetOwnerContacts)
            {
                if(email == petOwner._Email)
                {
                    return petOwner;
                }
            }
            return null;
        }

        private static double GetMedian(int[] sourceNumbers)
        {
            //Framework 2.0 version of this method. there is an easier way in F4        
            if (sourceNumbers == null || sourceNumbers.Length == 0)
                throw new System.Exception("Median of empty array not defined.");

            //make sure the list is sorted, but use a new array
            double[] sortedPNumbers = (double[])sourceNumbers.Clone();
            Array.Sort(sortedPNumbers);

            //get the median
            int size = sortedPNumbers.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            return median;
        }

        public void CalculateMedian()
        {
            if (Comments.Count == 0)
            {
                throw new System.NotImplementedException();
            }
            else {
                int[] allRates = { };
                Console.WriteLine("My Calculated Rate Median is:");
                for (int i = 0; i < Comments.Count; i++)
                {
                    allRates[i] = Comments[i]._Star;
                }
                GetMedian(allRates);
            }
         
        }

        public void CalculateAverage()
        {
            if(Comments.Count == 0)
            {
                throw new System.NotImplementedException();
            }
            else
            {
                double counter = 0;
                Console.WriteLine("My Calculated Rate Average is:");
                for (int i = 0; i < Comments.Count; i++)
                {
                    counter += Comments[i]._Star;
                }
                counter = counter / Comments.Count;
            }
           
        }

        public override String ToString()
        {
            return Name + " " + Surname + "\n" + Bio;
        }
    }
}