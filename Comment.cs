using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    
    public class Comment
    {
        private DateTime Date;
        private String SenderName;
        private int Star;
        private String Text;

        public Comment(String senderName, int star, String text)
        {
            Date = DateTime.Now;
            SenderName = senderName;
            Star = star;
            Text = text;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}