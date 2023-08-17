using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
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

                listLive = findNodeRoad(xDocumentLive, wsdl, xs, choosenNode, choosenPath);

                listTest = findNodeRoad(xDocumentTest, wsdl, xs, choosenNode, choosenPath);

                Console.WriteLine("----Farklar----");

                Console.WriteLine("\n----Dosya 1----\n");

                var firstNotSecond = listLive.Where(i => !listTest.Contains(i)).ToList();

                foreach (var item in firstNotSecond)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n----Dosya 2----\n");

                var secondNotFirst = listTest.Where(i => !listLive.Contains(i)).ToList();

                foreach (var item in secondNotFirst)
                {
                    Console.WriteLine(item);
                }

            }





        }

        private static List<string> findNodeRoad(XDocument xDocumentLive, XNamespace wsdl, XNamespace xs, string choosenNode , string choosenPath)
        {
            Console.WriteLine("--------");

            List<string> fullNodeRoad = new List<string>();

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            {
                foreach (XElement xElement in xElementLive.Descendants(wsdl + "operation"))
                {

                    string xElementToString = xElement.FirstAttribute.Value.ToString();

                    if (xElementToString == choosenNode)
                    {
                        Console.WriteLine("Buldum " + xElementToString);

                        fullNodeRoad.Add(choosenNode);

                        XElement xElementOutput = xElement.Element(wsdl + choosenPath);

                        XElement xElementOutputToParse = XElement.Parse(xElementOutput.ToString());

                        string xElementOutputToString = xElementOutputToParse.FirstAttribute.ToString();

                        string xElementOutputToSubstring = xElementOutputToString.Substring(xElementOutputToString.LastIndexOf("/"));

                        string xElementOutputToSubstringDouble = xElementOutputToSubstring.Substring(1, xElementOutputToSubstring.Length - 2);

                        choosenNode = xElementOutputToSubstringDouble;

                        Console.WriteLine(choosenNode);
                        
                        fullNodeRoad.Add(choosenNode);

                        break;
                    }

                }
            }

            Console.WriteLine("--------");

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "types"))
            {
                foreach (XElement xElement1 in xElementLive.Descendants(xs + "element"))
                {

                    string xElementToString = xElement1.FirstAttribute.ToString();

                    if (xElementToString.Contains("minOccurs"))
                    {
                        continue;
                    }
                    else
                    {
                        string xElementToSubstring = xElementToString.Substring(xElementToString.LastIndexOf("=") + 1);

                        string xElementToSubstringDouble = xElementToSubstring.Substring(1, xElementToSubstring.Length - 2);

                        //Console.WriteLine(xElementToSubstringDouble);

                        if (xElementToSubstringDouble == choosenNode)
                        {
                            Console.WriteLine("Buldum " + xElementToSubstringDouble);

                            foreach (XElement xElement in xElement1.Descendants(xs + "element"))
                            {
                                string xElementString = xElement.Attribute("type").Value;
                                string propName = xElement.Attribute("name").Value;

                                string xElementStringToSubstring = xElementString.Substring(xElementString.LastIndexOf(":") + 1);


                                if (xElementString.Contains("tns:"))
                                {
                                    choosenNode = propName;
                                }

                                Console.WriteLine(xElementStringToSubstring);
                                
                                fullNodeRoad.Add(choosenNode);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("--------");


            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "types"))
            {
                foreach (XAttribute xElement1 in xElementLive.Descendants().Attributes("name"))
                {
                    string xElement1ToSubstringDouble = xElement1.Value; 

                    if (xElement1ToSubstringDouble.Equals(choosenNode))
                    {
                        Console.WriteLine("Buldum " + xElement1ToSubstringDouble);

                        foreach (XElement xTypeElement in xElement1.Parent.Descendants().Where(n => n.Name.LocalName == "element"))
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

                                        Console.WriteLine(xElementTypeToSubstringDouble + " name = " + xAttribute.Value + " nillable = " + nillableValue);

                                        fullNodeRoad.Add(xElementTypeToSubstringDouble + " name = " + xAttribute.Value + " nillable = " + nillableValue);
                                    }
                                    else
                                    {
                                        Console.WriteLine(xElementTypeToString + " name = " + xAttribute.Value + " nillable = " + nillableValue);

                                        fullNodeRoad.Add(xElementTypeToString + " name = " + xAttribute.Value + " nillable = " + nillableValue);
                                    }
                                }
                                else
                                {
                                    if (xElementTypeToString.Contains("tns:"))
                                    {
                                        string xElementTypeToSubstringDouble = xElementTypeToString.Substring(4);

                                        choosenNode = xElementTypeToSubstringDouble;

                                        Console.WriteLine(xElementTypeToSubstringDouble + " name = " + xAttribute.Value);

                                        fullNodeRoad.Add(xElementTypeToSubstringDouble + " name = " + xAttribute.Value);
                                    }
                                    else
                                    {
                                        Console.WriteLine(xElementTypeToString + " name = " + xAttribute.Value);

                                        fullNodeRoad.Add(xElementTypeToString + " name = " + xAttribute.Value);
                                    }
                                }
                            }
                        }

                        Console.WriteLine("--------");
                    }
                }
            }

            Console.WriteLine("----Blok Sonu----");
            return fullNodeRoad;
            
        }
    }
}

