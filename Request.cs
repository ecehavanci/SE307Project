using System.Collections;

namespace SE307Project
{
    class Request
    {
        private string RequestOwnerName;
        private ArrayList RequestedPets;

        public Request()
        {
            RequestedPets = new ArrayList();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}