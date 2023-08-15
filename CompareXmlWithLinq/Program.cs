
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

            string resultOutputILiveSportsBS = null;




            //IEnumerable<XElement> childlist = from el in xDocumentLive.Root.Element()
            //                                  select el;


            //IEnumerable<XElement> childlist2 = from el in xDocumentTest.Root.Elements().Elements()
            //                                   select el;



            //foreach (XElement childlive in childlist)
            //{
            //    Console.WriteLine(childlive);
            //    Console.WriteLine("------------");
            //}


            //foreach (XElement childTest in childlist2)
            //{
            //    Console.WriteLine(childTest);
            //    Console.WriteLine("------------");
            //}


            //foreach (XElement child in childGetLive)
            //{
            //    Console.WriteLine(child);
            //}

            string choosenNode = "GetLiveSports";


            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";

            Console.WriteLine("--------output--------");

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "output"))
            {

                XElement xElement = XElement.Parse(xElementLive.ToString());

                string result = xElement.FirstAttribute.ToString();

                string lastResult = result.Substring(result.LastIndexOf("/") + 1);

                string lastlastResult = lastResult.Substring(0, lastResult.Length - 1);

                Console.WriteLine(lastlastResult);

            }


            




            //foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            //{

            //    XElement xElement = XElement.Parse(xElementLive.ToString());

            //    string result = xElement.FirstAttribute.ToString();

            //    string lastResult = result.Substring(result.LastIndexOf("/") + 1);

            //    string lastlastResult = lastResult.Substring(0, lastResult.Length - 1);

            //    Console.WriteLine(lastlastResult);

            //}

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "portType"))
            {
                foreach(XElement xElement in xElementLive.Descendants(wsdl + "operation")){
                    
                    XElement xElementParse = XElement.Parse(xElement.ToString());

                    string xElementToString = xElementParse.FirstAttribute.ToString();

                    string xElementToSubstring = xElementToString.Substring(xElementToString.LastIndexOf("=") + 1);

                    string xElementToSubstringDouble = xElementToSubstring.Substring(1, xElementToSubstring.Length - 2);


                    if (xElementToSubstringDouble == choosenNode)
                    {
                        Console.WriteLine("Buldum " + xElementToSubstring);

                        choosenNode = xElementToSubstringDouble;

                        XElement xElementOutput = xElementParse.Element(wsdl + "output");

                        XElement xElement1 = XElement.Parse(xElementOutput.ToString());

                        string resultOutput = xElement1.FirstAttribute.ToString();

                        string lastResultOutput = resultOutput.Substring(resultOutput.LastIndexOf("/"));

                        string lastlastResultOutput = lastResultOutput.Substring(1, lastResultOutput.Length - 2);

                        resultOutputILiveSportsBS = lastlastResultOutput;

                        Console.WriteLine(lastlastResultOutput);

                        break;
                     
                        


                    }                    
                }
            }
            
        }
    }

}
