using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SE307Project
{
    [XmlRoot("Request")]
    public class Request
    {
        
        public DateTime Date;

        public PetOwner RequestOwner;

        public ArrayList RequestedPets;
        
        public bool IsAccepted = false;

        public Request()
        {
            Date = DateTime.Now;
            RequestOwner = new PetOwner();
            RequestedPets = new ArrayList();
            IsAccepted = false;
        }

        
        public Request(PetOwner requestOwner, ArrayList requestedPets)
        {
            RequestOwner = requestOwner;
            Date = DateTime.Now;
            RequestedPets = requestedPets;
        }
        
        public override string ToString()
        {
            string content = "********* Request Content *********";
            content += "Request Owner Info:" + RequestOwner.Name + " " + RequestOwner.Surname+"\n";
            content += "Desired Location:\t" + RequestOwner.Location+"\n";
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