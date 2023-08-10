using System.Xml.Linq;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            

            XDocument xDocumentLive = XDocument.Load("GetLiveSportsLive.xml");
            XDocument xDocumentTest = XDocument.Load("GetLiveSportsTest.xml");

           
            Console.WriteLine("-------------------------------------------------------------------");
            
            IEnumerable<XElement> childlist = from el in xDocumentLive.Root.Elements().Elements().Elements()
                                              select el;


            //foreach (XElement childlive in childlist)
            //{
            //    Console.WriteLine(childlive);
            //    Console.WriteLine("------------");
            //}

            //Console.WriteLine("------------");

            IEnumerable<XElement> childlist2 = from el in xDocumentTest.Root.Elements().Elements().Elements()
                                               select el;



            //foreach (XElement childTest in childlist2)
            //{
            //    Console.WriteLine(childTest);
            //}

            //Console.WriteLine("------------");


            foreach (XElement child in childlist)
            {
                bool result = childlist2.Contains(child);

                if (result = true)
                {

                }
                else if(result =)
                {
                    Console.WriteLine("----" + result + "----");
                    Console.WriteLine(child);
                }

            }







            //foreach (XElement el in difference)
            //{
            //    Console.WriteLine(el.ToString());
            //}




            //foreach(XElement nodeLoop in childlist)
            //{
            //    foreach(XElement nodeLoop_2 in childlist2)
            //    {
            //        var differenceNode = 
            //            from el in nodeLoop_2.Elements() ==
            //    }
            //}




        }

        


       
    }

        
}

