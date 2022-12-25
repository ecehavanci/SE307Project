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
        
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
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
        public void AcceptRequest(Request request) ///////////////////////////
        {
            Console.WriteLine("Request Accepted!");
        }
        public void RejectRequest(Request request) ///////////////////////////
        {
            Console.WriteLine("Request Rejected!");
        }

        public override void EditProfile()
        {

        }
        public void ShowCommentsAndRates()
        {

        }

        public override void ShowMessagesFor(String email)
        {

        }

        public void CalculateMedian()
        {
            //FOR STARS
            //throw new System.NotImplementedException();
        }

        public void CalculateAverage()
        {
            //FOR STARS
            //throw new System.NotImplementedException();
        }

        public override String ToString()
        {
            return Name + " " + Surname + "\n" + Bio;
        }
    }
}