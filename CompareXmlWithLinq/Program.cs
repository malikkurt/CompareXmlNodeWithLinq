
using System;
using System.Xml;
using System.Xml.Linq;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Root listeleme


            XDocument xDocumentLive = XDocument.Load("GetLiveSportsLive.xml");
            XDocument xDocumentTest = XDocument.Load("GetLiveSportsTest.xml");

           /* IEnumerable<XElement> childList =
                from el in xDocumentLive.Root.Elements()
                select el;
            foreach (XElement child in childList)
            {
                Console.WriteLine(child.Name);
            }
           */
            // Root Listeleme

            Console.WriteLine("-------------------------------------------------------------------");

            // Node Listeleme


            
            IEnumerable<XElement> childlist = from el in xDocumentLive.Root.Elements()
                                              select el;


            //foreach (XElement childlive in childlist)
            //{
            //    Console.WriteLine(childlive);
            //}

            Console.WriteLine("------------");

            IEnumerable<XElement> childlist2 = from el in xDocumentTest.Root.Elements()
                                               select el;

            

            //foreach (XElement childTest in childlist2)
            //{
            //    Console.WriteLine(childTest);
            //}

            Console.WriteLine("------------");

            


            foreach(string nodeLoop in childlist)
            {
                foreach(string nodeloop_2 in childlist2)
                {
                    if(nodeLoop == nodeloop_2)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Fail");
                    }
                }
            }

           


        }

        


       
    }

        
}

