
using System;
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



            //foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            //{

            //    XElement xElement = XElement.Parse(xElementLive.ToString());

            //    string result = xElement.FirstAttribute.ToString();

            //    string lastResult = result.Substring(result.LastIndexOf("/") + 1);

            //    string lastlastResult = lastResult.Substring(0, lastResult.Length - 1);

            //    Console.WriteLine(lastlastResult);

            //}

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

                        XElement xElementOutputParse = XElement.Parse(xElementOutput.ToString());

                        string resultOutput = xElementOutputParse.FirstAttribute.ToString();

                        string lastResultOutput = resultOutput.Substring(resultOutput.LastIndexOf("/"));

                        string lastlastResultOutput = lastResultOutput.Substring(1, lastResultOutput.Length - 2);

                        choosenNode = lastlastResultOutput;

                        Console.WriteLine(lastlastResultOutput);

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

                        Console.WriteLine(xElementToSubstringDouble);

                        if (xElementToSubstringDouble == choosenNode)
                        {
                            foreach (XElement xElement in xElement1.Descendants(xs + "element"))
                            {
                                string xElementString = xElement.Attribute("type").Value;

                                string xElementStringToSubstring = xElementString.Substring(xElementString.LastIndexOf(":") + 1);

                                Console.WriteLine("Buldum: " + xElementStringToSubstring);

                                choosenNode = xElementString;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("--------");

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "types"))
            {
                foreach (XElement xElement1 in xElementLive.Descendants(xs + "complexType"))
                {

                    string xElementToString = xElement1.FirstAttribute.ToString();

                    if(xElementToString.Contains("minOccurs"))
                    {
                        string xElementToSubstring = xElementToString.Substring(xElementToString.LastIndexOf("=") + 1);

                        string xElementToSubstringDouble = xElementToSubstring.Substring(1, xElementToSubstring.Length - 2);

                        Console.WriteLine(xElementToSubstringDouble);

                    }
                    else
                    {
                        continue;
                    }

                    


                  
                   
                        

                        

                        
                    }
                }
            }




        }
}

