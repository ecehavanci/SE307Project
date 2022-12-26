using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace SE307Project
{
    public class FileHandler
    {
        public void WriteUser(string filename, User user)
            {
                XmlSerializer x = new XmlSerializer(user.GetType());
                Stream fs = new FileStream(filename, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
                x.Serialize(writer, user);
                writer.Close();
            }

            public User ReadUser(string filename)
            {
                Stream reader = new FileStream(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(User));

                User user = (User)serializer.Deserialize(reader);
                return user;
            }
        
        
        /*public void saveToCSV()
        {
            // T var = (T)(object)obj;


             var configUser = new CsvConfiguration(CultureInfo.InvariantCulture)
             {
                 HasHeaderRecord = false
             };

             using (var stream = File.Open("users.csv", FileMode.Append))
             using (var writer = new StreamWriter(stream))
             using (var csv = new CsvWriter(writer, configUser))
             {

                 csv.WriteRecords(users);*/
            /*var myPersons = new List<Person>()
            {
                new Person { Id = 1, IsLiving = true, Name = "John", DateOfBirth = Convert.ToDateTime("03/05/2006") },
                new Person { Id = 2, IsLiving = true, Name = "Steve", DateOfBirth = Convert.ToDateTime("03/09/1998") },
                new Person { Id = 3, IsLiving = true, Name = "James", DateOfBirth = Convert.ToDateTime("03/08/1994") }
            };

            using (var writer = new StreamWriter("filePersonsWithDoB.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(myPersons);
            } 

        }*/

        /*private User readFromCSV()
        {
            User record = null;

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                IncludePrivateMembers = true,
            };

            using (var reader = new StreamReader("users.csv"))
            using (var csv = new CsvReader(reader, configuration))
            {
                record = csv.GetRecord<User>();

            }
            return record;
        }*/
    }
}
