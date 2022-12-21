using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    enum AcceptionEnum
    {
        Waiting,
        Accepted,
        Rejected
    }

    class RequestBox
    {
        public List<Request> Requests { get; set; }
        public User Owner { get; set; }
        public AcceptionEnum AcceptionEnum { get; set; }

        public RequestBox(User Owner, AcceptionEnum acceptionType)
        {
            Requests = new List<Request>();
            this.Owner = Owner;
            AcceptionEnum = acceptionType;
        }

        public virtual void DisplayRequestBox()
        {
            Console.WriteLine("----------- "+AcceptionEnum+ "Request Box -----------");
            if (Requests.Count == 0)
                Console.WriteLine("No Requests here!\n");
            else
            {
                for (int i = 0; i < Requests.Count; i++)
                {
                    Console.Write((i + 1) + ") ");
                    Requests[i].ToString();
                }
            }
        }

        public Request ReadRequest(int RequestNo )
        {
            if (RequestNo >= Requests.Count || RequestNo < 0)
                throw new Exception("Incorrect Request selection!");
            Console.WriteLine(Requests[RequestNo].ToString());
            return Requests[RequestNo];
        }

    }
}
