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
        private List<PetOwner> PetOwnerContacts;
        private List<Request> Requests;

        public PetSitter(string name, string surname, string email, string password, string Location):base(name,surname,email,password,Location)
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

        public void AcceptRequest(Request request)
        {

        }
        public void DeclineRequest(Request request)
        {

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
            //throw new System.NotImplementedException();
        }

        public void CalculateAverage()
        {
            //throw new System.NotImplementedException();
        }
    }
}