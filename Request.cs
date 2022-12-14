using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SE307Project
{
    public class Request
    {
        public DateTime Date;

        public String RequestOwnerName;
        
        public String RequestOwnerEmail;

        public String Location;

        public List<Pet> RequestedPets;

        public bool IsAccepted = false;

        public Request()
        {
            Date = DateTime.Now;
            RequestOwnerName = "";
            RequestedPets = new List<Pet>();
            IsAccepted = false;
            Location = "";
            RequestOwnerEmail = "";
        }


        public Request(String requestOwnerName, String requestOwnerEmail, List<Pet> requestedPets, String location)
        {
            RequestOwnerName = requestOwnerName;
            RequestOwnerEmail = requestOwnerEmail;
            Date = DateTime.Now;
            RequestedPets = requestedPets;
            Location = location;
        }

        public override string ToString() //to print the Request properties
        {
            String location = String.IsNullOrEmpty(Location) ? "N/A" : Location;
            string content = RequestOwnerName + " sent you a request to take care of " +
                             RequestedPets.Count + " of his/her pets. Pets are:\n";
            foreach (Pet pet in RequestedPets)
            {
                content += pet.ToString();
            }

            content += "Their Location:\t" + location + "\n";


            content += "****************************";
            return content;
        }
    }
}