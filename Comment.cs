using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SE307Project
{
    public class Comment
    {
        public Comment(){}
        
        public DateTime Date;
        
        public String SenderName;

        public int Star;
        
        public String Text;

        public Comment(String senderName, int star, String text)
        {
            Date = DateTime.Now;
            SenderName = senderName;
            Star = star;
            Text = text;
        }

        public override string ToString()//print the comment's details
        {
            string print = "-----------------------------------";
            print += "From:\t" + SenderName + "                    ("+Date+")";
            print += "Rate:\t" + ConvertToStars();
            print += "Comment:\t" + Text;
            return print;
        }

        private string ConvertToStars() //print comment's stars 
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