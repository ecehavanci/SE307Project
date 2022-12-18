using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class PetSitter : User
    {
        private List<Comment> Comments;
        private List<PetOwner> PetOwnerContacts;

        public PetSitter(string name, string surname, string email, string password, string Location):base(name,surname,email,password,Location)
        {
            this.Comments = new List<Comment>();
            this.PetOwnerContacts = new List<PetOwner>();
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

        public void EditProfile()
        {

        }
        public void ReadRequests()
        {
            
        }

        public void ShowCommentsAndRates()
        {

        }

        public void ShowMessageFor(string UserName)
        {

        }

    }
}