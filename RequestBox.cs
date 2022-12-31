﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public enum StatusEnum
    {
        Waiting,
        Accepted,
        Rejected
    }

    public class RequestBox
    {
        public List<Request> Requests { get; set; }
        public StatusEnum StatusEnum { get; set; }

        public RequestBox(StatusEnum status)
        {
            Requests = new List<Request>();
            StatusEnum = status;
        }

        public RequestBox()
        {
            Requests = new List<Request>();
            StatusEnum = StatusEnum.Waiting;
        }
        public void DisplayRequestBox()
        {
            Console.WriteLine("----------- "+StatusEnum+ " Request Box -----------");
            if (Requests.Count == 0)
                Console.WriteLine("No Requests here!\n");
            else
            {
                for (int i = 0; i < Requests.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") ");
                    Console.WriteLine(Requests[i].ToString());
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

        public void ReceiveRequest(Request request)
        {
            Requests.Add(request);
            Console.WriteLine("Success!");
        }

        public Request MoveMailToAnotherBox(int selection)
        {
            Request r = Requests[selection];
            Requests.Remove(r);
            return r;
        }

        public void SortByDateDesc()
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                int j = i;
                while (j > 0 && Requests[j - 1].Date < Requests[j].Date)
                {
                    Request temp = Requests[j - 1];
                    Requests[j - 1] = Requests[j];
                    Requests[j] = temp;
                    j--;
                }
            }
        }

        public void SortByDateAsc()
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                int j = i;
                while (j > 0 && Requests[j - 1].Date > Requests[j].Date)
                {
                    Request temp = Requests[j - 1];
                    Requests[j - 1] = Requests[j];
                    Requests[j] = temp;
                    j--;
                }
            }
        }
    }
}
