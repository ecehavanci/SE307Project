using System;
using System.Collections;

namespace SE307Project
{
    public class Request
    {
        public DateTime Date;
        public String RequestOwnerEmail;
        public ArrayList RequestedPets;
        public bool IsAccepted = false;
        public bool _IsAccepted
        {
            get { return IsAccepted; }
            set { IsAccepted = _IsAccepted; }
        }
        public DateTime _Date
        {
            get { return Date; }
            set { Date = _Date; }
        }

        public Request(string requestOwnerEmail, ArrayList requestedPets)
        {
            RequestOwnerEmail = requestOwnerEmail;
            Date = DateTime.Now;
            RequestedPets = requestedPets;
        }

        public Request()
        {
            
        }
        
        /*public override string ToString()
        {
            string content = "********* Request Content *********";
            content += "Request Owner Info:" + RequestOwner._Name + " " + RequestOwner._Surname+"\n";
            content += "Desired Location:\t" + RequestOwner._Location+"\n";
            content += "Number Of Pets:\t"+RequestedPets.Count;
            foreach(Pet pet in RequestedPets)
            {
               content += pet.ToString();
            }
            content += "****************************";
            return content;
        }*/
    }
}