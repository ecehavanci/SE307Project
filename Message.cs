using System;

namespace SE307Project
{
    public class Message
    {
        private DateTime Date;
        private String SenderMail;
        private String Text;

        public Message(DateTime date, string senderMail, string text)
        {
            this.Date = date;
            SenderMail = senderMail;
            Text = text;
        }

        public override string ToString()
        {
            return "Sender:\t"+SenderMail+"\n Date:"+Date+"\nText:\t"+Text;
        }
    }
}