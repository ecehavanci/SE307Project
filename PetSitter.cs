using System;
using System.Collections.Generic;

namespace SE307Project
{
    public interface ICalculable
    {
        public void CalculateMedian();
        public void CalculateAverage();

    }
    
    public class PetSitter : User, ICalculable
    {
        private List<Comment> Comments;
        private List<PetOwner> PetOwnerContacts;//???
        private List<Request> Requests;
        private String Bio;

        public PetSitter(string name, string surname, string email, string password):base(name,surname,email,password)
        {
            Comments = new List<Comment>();
            PetOwnerContacts = new List<PetOwner>();
            Requests = new List<Request>();
        }
        
        public void AddRequest(Request request)
        {
            Requests.Add(request);
        }
        
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void ReceiveRequest(Request request)
        {
            Console.WriteLine("Request Received!");
        }

        public void AcceptRequest(Request request) ///////////////////////////
        {
            Console.WriteLine("Request Accepted!");
        }
        public void AcceptReject(Request request) ///////////////////////////
        {
            Console.WriteLine("Request Rejected!");
        }

        public override void EditProfile()
        {

        }
        public void ReadRequests()
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