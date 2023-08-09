
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> nodeList = new List<string>();

            // 1.dosya için node ayırma kısmı

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("GetLiveSportsLive.xml");

            XmlNodeList nodes = xmlDocument.SelectNodes("//*");
            foreach (XmlNode node in nodes)
            {
                XmlAttribute typeAttribute = node.Attributes["type"];
                if (typeAttribute != null)
                {
                    Console.WriteLine("Node: " + node.LocalName + ", Type: " + typeAttribute.Value);
                    nodeList.Add(typeAttribute.Value);
                }
            }

            // 2.dosya için node ayırma kısmı 

            List<string> nodeList2 = new List<string>();

            XmlDocument xmlDocument2 = new XmlDocument();
            xmlDocument2.Load("GetLiveSportsTest.xml");

            XmlNodeList nodes2 = xmlDocument2.SelectNodes("//*");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            foreach(XmlNode node2 in nodes2)
            {
                XmlAttribute typeAttribute2 = node2.Attributes["type"];
                if(typeAttribute2 != null)
                {
                    Console.WriteLine("Node: " + node2.Name + ", Type: " + typeAttribute2.Value);
                    nodeList2.Add(typeAttribute2.Value);
                }
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");

            IEnumerable<string> difference = nodeList2.Except(nodeList);

            foreach(string node2 in difference)
            {
                Console.WriteLine(node2);
            }



            HashSet<string> hashSet1 = new HashSet<string>(nodeList);
            HashSet<string> hashSet2 = new HashSet<string>(nodeList2);

            hashSet2.ExceptWith(hashSet1);

            foreach(string node2 in hashSet1)
            {
                Console.WriteLine(node2);
            }



            
            
        
    



}
    }

        
    }

