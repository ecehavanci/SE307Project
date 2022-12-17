using System;
using System.Collections;

namespace SE307Project
{
    public class Request
    {
        private string RequestOwnerName;
        private ArrayList RequestedPets;

        public Request()
        {
            RequestedPets = new ArrayList();
        }
        
        public Request(String requestOwnerName, ArrayList pets)
        {
            RequestedPets = pets;
            RequestOwnerName = requestOwnerName;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}