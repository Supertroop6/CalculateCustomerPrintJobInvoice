using System;

namespace ProcessCustomerPrintOrders
{
    class Program
    {
        static void Main(string[] args)
        {
            Batch batch = new Batch();
            Console.WriteLine("Welcome to the Inner Working Batch Job Application");
            Console.WriteLine("To execute a batch job type: start batch");

            string command = Console.ReadLine();

            if (command.Trim().ToLower() == "start batch")
            {
                Console.Clear();
                Console.WriteLine("Innerworking Batch Run" + Environment.NewLine);
                batch.ReadInputFile();
                batch.ProcessAllJobs();
                batch.PrintInvoice();
                Console.WriteLine("Batch ran successfully.");
            }
            else
            {
                Console.WriteLine("Exiting batch application.");
            }

            Console.WriteLine("Press enter to close applicaiton.");
            Console.ReadLine();
        }
    }
}
