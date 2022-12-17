using System;

namespace SE307Project
{
    public class Message
    {
        private DateTime Date;
        private String SenderMail;
        private String ReceiverMail;
        private String Text;

        public Message(String senderMail, String receiverMail, String text)
        {
            Date = DateTime.Now;
            SenderMail = senderMail;
            ReceiverMail = receiverMail;
            Text = text;
        }

        public override string ToString()
        {
            return "Sender:\t"+SenderMail+"\n Date:"+Date+"\nText:\t"+Text;
        }
    }
}