
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


            XDocument xDocumentLive = XDocument.Load("GetLiveSportsLive.xml");
            XDocument xDocumentTest = XDocument.Load("GetLiveSportsTest.xml");

            Console.WriteLine("Enter Node Road:");
            string choosenNode = Console.ReadLine();

     

            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";

            XNamespace xs = "http://www.w3.org/2001/XMLSchema";


            Console.WriteLine("--------");

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            {
                foreach (XElement xElement in xElementLive.Descendants(wsdl + "operation"))
                {
                    string xElementToString = xElement.FirstAttribute.ToString();

                    string xElementToSubstring = xElementToString.Substring(xElementToString.LastIndexOf("=") + 1);

                    string xElementToSubstringDouble = xElementToSubstring.Substring(1, xElementToSubstring.Length - 2);

                    if (xElementToSubstringDouble == choosenNode)
                    {
                        Console.WriteLine("Buldum " + xElementToSubstring);

                        XElement xElementOutput = xElement.Element(wsdl + "output");

                        XElement xElementOutputToParse = XElement.Parse(xElementOutput.ToString());

                        string xElementOutputToString = xElementOutputToParse.FirstAttribute.ToString();

                        string xElementOutputToSubstring = xElementOutputToString.Substring(xElementOutputToString.LastIndexOf("/"));

                        string xElementOutputToSubstringDouble = xElementOutputToSubstring.Substring(1, xElementOutputToSubstring.Length - 2);

                        choosenNode = xElementOutputToSubstringDouble;

                        Console.WriteLine(choosenNode);

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


                                choosenNode = xElementStringToSubstring;

                                Console.WriteLine(choosenNode + propName);
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

                        string xElement1ToString = xElement1.ToString();

                        string xElement1ToSubstring = xElement1ToString.Substring(xElement1ToString.LastIndexOf("=") + 1);

                        string xElement1ToSubstringDouble = xElement1ToSubstring.Substring(1, xElement1ToSubstring.Length - 2);


                        if (xElement1ToSubstringDouble.Equals(choosenNode))
                        {
                            Console.WriteLine("Buldum " + xElement1ToSubstringDouble);

                            foreach (XAttribute xElementType in xElement1.Parent.Descendants().Attributes("type"))
                            {
                                string xElementTypeToString = xElementType.ToString();

                                foreach(XAttribute xAttribute in xElement1.Parent.Descendants().Attributes("name"))
                                {


                                    if (xElementTypeToString.Contains("tns:"))
                                    {
                                        string xElementTypeToSubstring = xElementTypeToString.Substring(xElementTypeToString.LastIndexOf(":") + 1);

                                        string xElementTypeToSubstringDouble = xElementTypeToSubstring.Substring(0, xElementTypeToSubstring.Length - 1);

                                        choosenNode = xElementTypeToSubstringDouble;

                                        Console.WriteLine(xElementTypeToSubstringDouble +" " + xAttribute);
                                    }
                                    else
                                    {
                                        Console.WriteLine(xElementTypeToString + " " + xAttribute);
                                    }




                                }

                                


                                
                            }
                            Console.WriteLine("--------");
                
                            
                        }
                    }
                }
        }
    }
}

