using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                XDocument xDocumentLive = XDocument.Load("MALIK_LIVE.xml");
                XDocument xDocumentTest = XDocument.Load("MALIK_TEST.xml");

                List<string> listLive = new List<string>();
                List<string> listTest = new List<string>();

                Console.WriteLine("Enter Node Road:");
                string choosenNode = Console.ReadLine();

                Console.WriteLine("Enter input or output");
                string choosenPath = Console.ReadLine();

                XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
                XNamespace xs = "http://www.w3.org/2001/XMLSchema";

                listLive = findNodeRoad(xDocumentLive, choosenNode, choosenPath);

                listTest = findNodeRoad(xDocumentTest, choosenNode, choosenPath);

                Console.WriteLine("----Live Dosyası----");
                printNodeRoad(listLive);

                Console.WriteLine("----Test Dosyası----");
                printNodeRoad(listTest);

                Console.WriteLine("----Farklar----");

                Console.WriteLine("\n----Live Dosyası----\n");

                var firstNotSecond = listLive.Where(i => !listTest.Contains(i)).ToList();

                foreach (var item in firstNotSecond)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n----Test Dosyası----\n");

                var secondNotFirst = listTest.Where(i => !listLive.Contains(i)).ToList();

                foreach (var item in secondNotFirst)
                {
                    Console.WriteLine(item);
                }

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("LiveDosyasında Olup Test Dosyasında olmayanlar");
             
                foreach (string item in firstNotSecond)
                {
                    foreach (char c in item)
                    {
                        sb.Append(System.Convert.ToInt32(c).ToString()); 
                        sb.Append(" ");
                    }
                   
                    
                }

                File.WriteAllText("difference.txt", sb.ToString(),Encoding.UTF8);

            }
        }


        private static void printNodeRoad(List<string> listLive)
        {
            foreach(var item in listLive)
            {
                Console.WriteLine(item);
            }
        }

        private static List<string> findNodeRoad(XDocument xDocumentLive, string choosenNode , string choosenPath)
        {

            List<string> fullNodeRoad = new List<string>();

            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";

            XNamespace xs = "http://www.w3.org/2001/XMLSchema";

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            {
                foreach (XElement xElement in xElementLive.Descendants(wsdl + "operation"))
                {

                    string xElementToString = xElement.FirstAttribute.Value.ToString();

                    if (xElementToString == choosenNode)
                    {
                        // Console.WriteLine("Buldum " + xElementToString);

                        fullNodeRoad.Add(choosenNode);

                        XElement xElementOutput = xElement.Element(wsdl + choosenPath);

                        XElement xElementOutputToParse = XElement.Parse(xElementOutput.ToString());

                        string xElementOutputToString = xElementOutputToParse.FirstAttribute.ToString();

                        string xElementOutputToSubstring = xElementOutputToString.Substring(xElementOutputToString.LastIndexOf("/"));

                        string xElementOutputToSubstringDouble = xElementOutputToSubstring.Substring(1, xElementOutputToSubstring.Length - 2);

                        choosenNode = xElementOutputToSubstringDouble;

                        // Console.WriteLine(choosenNode);
                        
                        fullNodeRoad.Add(choosenNode);

                        break;
                    }

                }
            }

            bool loopFlag = true;

            while (loopFlag)
            {
                loopFlag = false;

                foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "types"))
                {
                    foreach (XElement xElement1 in xElementLive.Descendants(xs + "schema").Elements())
                    {
                        string xElement1ToSubstringDouble = xElement1.FirstAttribute.Value;

                        if (xElement1ToSubstringDouble.Equals(choosenNode))
                        {
                            // Console.WriteLine("Buldum " + xElement1ToSubstringDouble);

                            foreach (XElement xTypeElement in xElement1.Descendants().Where(n => n.Name.LocalName == "element"))
                            {
                                XAttribute xElementType = xTypeElement.Attribute("type");
                                XAttribute xAttribute = xTypeElement.Attribute("name");
                                XAttribute xAttributeNıllable = xTypeElement.Attribute("nillable");

                                if (xElementType != null && xAttribute != null)
                                {
                                    string xElementTypeToString = xElementType.Value;

                                    if (xAttributeNıllable != null && !string.IsNullOrEmpty(xAttributeNıllable.Value))
                                    {
                                        string nillableValue = xAttributeNıllable.Value;

                                        if (xElementTypeToString.Contains("tns:"))
                                        {
                                            string xElementTypeToSubstringDouble = xElementTypeToString.Substring(4);

                                            choosenNode = xElementTypeToSubstringDouble;

                                            loopFlag = true;

                                            // Console.WriteLine(xElementTypeToSubstringDouble + " name = " + xAttribute.Value + " nillable = " + nillableValue);

                                            fullNodeRoad.Add(xElementTypeToSubstringDouble + " name = " + xAttribute.Value + " nillable = " + nillableValue);
                                        }
                                        else
                                        {
                                            // Console.WriteLine(xElementTypeToString + " name = " + xAttribute.Value + " nillable = " + nillableValue);

                                            fullNodeRoad.Add(xElementTypeToString + " name = " + xAttribute.Value + " nillable = " + nillableValue);
                                        }
                                    }
                                    else
                                    {
                                        if (xElementTypeToString.Contains("tns:"))
                                        {
                                            string xElementTypeToSubstringDouble = xElementTypeToString.Substring(4);

                                            choosenNode = xElementTypeToSubstringDouble;

                                            loopFlag = true;

                                            // Console.WriteLine(xElementTypeToSubstringDouble + " name = " + xAttribute.Value);

                                            fullNodeRoad.Add(xElementTypeToSubstringDouble + " name = " + xAttribute.Value);
                                        }
                                        else
                                        {
                                            // Console.WriteLine(xElementTypeToString + " name = " + xAttribute.Value);

                                            fullNodeRoad.Add(xElementTypeToString + " name = " + xAttribute.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return fullNodeRoad;
        }
    }
}

