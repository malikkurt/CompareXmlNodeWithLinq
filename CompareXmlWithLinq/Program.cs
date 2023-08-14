
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


            //XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
            //var portTypeName = "portType";
            //var esit = "=";
            //var name = "name";

            //IEnumerable<XElement> childGetLive = from rl in xDocumentLive.Descendants(wsdl + portTypeName + name + esit + "\"ILiveSportsBS\"")
            //                                     select rl;




            //foreach (XElement child in childGetLive)
            //{
            //    Console.WriteLine(child);
            //}

            string choosenNode = "GetLiveSports";

            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
             
            //foreach ( XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "input" ))
            //{
            //    Console.WriteLine(xElementLive.Name + " = " + xElementLive.Value + " = " + xElementLive.FirstAttribute);
            //}

            foreach (XElement xElementLive in xDocumentLive.Root.Descendants().Where(n => n.Name == wsdl + "input"))
            {
                
                XElement xElement = XElement.Parse(xElementLive.ToString());

                string result = xElement.FirstAttribute.ToString();

                string lastResult = result.Substring(result.LastIndexOf("/")+ 1);

                string lastlastResult = lastResult.Substring(0, lastResult.Length - 1);

                Console.WriteLine(lastlastResult);
                
            }












        }
    }

}
