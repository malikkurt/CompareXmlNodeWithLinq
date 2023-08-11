
using System;
using System.Runtime.Serialization.Json;
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





            IEnumerable<XElement> childlist = from el in xDocumentLive.Root.Elements().Elements()
                                              select el;
            
            
            IEnumerable<XElement> childlist2 = from el in xDocumentTest.Root.Elements().Elements()
                                              select el;



            foreach (XElement childlive in childlist)
            {
                Console.WriteLine(childlive);
                Console.WriteLine("------------");
            }


            //foreach (XElement childTest in childlist2)
            //{
            //    Console.WriteLine(childTest);
            //    Console.WriteLine("------------");
            //}


            var liveElements = xDocumentLive.Descendants("xs:element");


            //var missingElements = liveElements.Where(liveElem => !testElements.Any(testElem => testElem.Attribute("name").Value == liveElem.Attribute("name").Value));
            //foreach (var missingElement in missingElements)
            //{
            //    differences.Add("Element " + missingElement.Attribute("name").Value + " is missing in test WSDL.");
            //}

            //// Compare sequence differences
            //foreach (var liveElement in liveElements)
            //{
            //    string elementName = liveElement.Attribute("name").Value;
            //    var testElement = testElements.FirstOrDefault(testElem => testElem.Attribute("name").Value == elementName);

            //    if (testElement != null)
            //    {
            //        var liveSequence = liveElement.Descendants("xs:sequence").FirstOrDefault();
            //        var testSequence = testElement.Descendants("xs:sequence").FirstOrDefault();

            //        if (liveSequence != null && testSequence != null)
            //        {
            //            bool sequenceMatch = XNode.DeepEquals(liveSequence, testSequence);
            //            if (!sequenceMatch)
            //            {
            //                differences.Add("Element " + elementName + " sequence is different between live and test WSDL.");
            //            }
            //        }
            //    }















            }

    }

}
