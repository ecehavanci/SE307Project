using System;
using System.Collections;

namespace SE307Project
{
    public class Request
    {
        private DateTime Date;
        private PetOwner RequestOwner;
        private PetSitter RequestTaker;
        private ArrayList RequestedPets;
        private bool IsAccepted = false;
        public bool _IsAccepted
        {
            get { return IsAccepted; }
            set { IsAccepted = _IsAccepted; }
        }

        public Request(PetOwner RequestOwner,PetSitter RequestTaker,DateTime date)
        {
            this.RequestTaker = RequestTaker;
            this.RequestOwner = RequestOwner;
            this.Date = date;
            RequestedPets = new ArrayList();
        }

        public override string ToString()
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
        }
    }
}