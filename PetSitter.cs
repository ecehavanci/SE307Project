using System.Collections.Generic;

namespace SE307Project
{
    class PetSitter : User
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

        public void AcceptRequest(Request request)
        {

        }
        public void DeclineRequest(Request request)
        {

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