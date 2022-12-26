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
        public int _Star
        {
            get { return Star; }
        }
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
            string print = "-----------------------------------";
            print +="Date:\t" + Date;
            print += "Sender:\t" + SenderName;
            print += "Rate:\t" + StarPrinter();
            print += "Comment:\t" + Text;
            return print;
        }

        private string StarPrinter()
        {
            switch (Star)
            {
                case 1:
                    return "*";
                case 2:
                    return "*_*";
                case 3:
                    return "*_*_*";
                case 4:
                    return "*_*_*_*";
                case 5:
                    return "*_*_*_*_*";
            }
            return null;
        }
    }
}