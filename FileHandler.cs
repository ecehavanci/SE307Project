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
    public class XMLHandler
    {
        public void WritePetOwnerList(string filename, List<PetOwner> po)
        {
            XmlSerializer x = new XmlSerializer(po.GetType());
            Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
            x.Serialize(writer, po);
            writer.Close();
        }

        public List<PetOwner> ReadPetOwnerList(string filename)
        {
            if (!File.Exists(filename))
            {
                return new List<PetOwner>();
            }
            Stream reader = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<PetOwner>));
            List<PetOwner> po = (List<PetOwner>) serializer.Deserialize(reader);
            reader.Close();
            return po;
        }
        
        
        public void WritePetSitterList(string filename, List<PetSitter> ps)
        {
            XmlSerializer x = new XmlSerializer(ps.GetType());
            Stream fs = new FileStream(filename, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
            x.Serialize(writer, ps);
            writer.Close();
        }

        public List<PetSitter> ReadPetSitterList(string filename)
        {
            if (!File.Exists(filename))
            {
                return new List<PetSitter>();
            }
            Stream reader = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<PetSitter>));
            List<PetSitter> ps = (List<PetSitter>) serializer.Deserialize(reader);
            reader.Close();
            return ps;
        }
    }
}
