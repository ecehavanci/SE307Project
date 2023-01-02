using System;
using System.Xml.Serialization;

namespace SE307Project
{
    [XmlInclude(typeof(HiringMessage))]
    public class Message
    {
        
        public DateTime Date;
        
        public String SenderMail;
        
        public String ReceiverMail;
        
        public String Text;

        
        public Message()
        {
            Date = DateTime.Now;
            SenderMail = "";
            ReceiverMail = "";
            Text = "";
        }

        
        public Message(String senderMail, String receiverMail, String text)
        {
            Date = DateTime.Now;
            SenderMail = senderMail;
            ReceiverMail = receiverMail;
            Text = text;
        }

        public override string ToString()//to print the mesage text
        {
          
            return Text; 
        }
    }

    
}