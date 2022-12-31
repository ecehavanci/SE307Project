using System;

namespace SE307Project
{
    public class Message
    {
        public DateTime Date;
        public String SenderMail;
        public String ReceiverMail;
        public String Text;

        public Message(String senderMail, String receiverMail, String text)
        {
            Date = DateTime.Now;
            SenderMail = senderMail;
            ReceiverMail = receiverMail;
            Text = text;
        }

        public Message()
        {
            
        }

        public override string ToString()
        {
            return "Sender:\t"+SenderMail+"\n Date:"+Date+"\nText:\t"+Text;
        }
    }
}