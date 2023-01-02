using System;
using System.Xml.Serialization;

namespace SE307Project
{
    public class HiringMessage : Message
    {
        public String RelatedEmail;

        public HiringMessage()
        {
            Date = DateTime.Now;
            SenderMail = "";
            ReceiverMail = "";
            Text = "";
            RelatedEmail = "";
        }

        public HiringMessage(String receiverEmail, String relatedEmail, int mode)
        {
            Database db = Database.GetInstance();
            Date = DateTime.Now;
            SenderMail = "system";
            ReceiverMail = receiverEmail;
            RelatedEmail = relatedEmail;
            User user = db.FindUser(relatedEmail);
            if (mode == 1)
            {
                Text = user == null
                    ? ""
                    : (user.Name + " " + user.Surname) + " wants to mark you as hired. Did this " +
                      "person hire you? (Y/N)";
            }
            else
            {
                Text = user == null
                    ? ""
                    : (user.Name + " " + user.Surname) + " has rejected your hiring request. Please " +
                      "contact us if this person is hired by you.";
            }
        }
    }
}