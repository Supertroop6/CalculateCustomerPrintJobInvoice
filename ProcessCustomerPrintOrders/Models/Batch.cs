using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProcessCustomerPrintOrders
{
    /// <summary>
    /// Represents a batch that contains all the jobs for a customer.
    /// </summary>
    class Batch : IBatchService
    {
        /// <summary>
        /// The path of the input file to process a batch.
        /// </summary>
        private readonly string rootFolder = Environment.CurrentDirectory + @"\";
        /// <summary>
        /// A collection of jobs ordered by a customer.
        /// </summary>
        public List<Job> Jobs { get; set; }
        /// <summary>
        /// Default construct for Batch
        /// </summary>
        public Batch()
        {
            Jobs = new List<Job>();
        }
        /// <summary>
        /// Process all customer jobs.
        /// </summary>
        public void ProcessAllJobs()
        {
            foreach (Job job in Jobs)
            {
                job.ProcessAllPrintItems();
                Console.Write(job.DisplayLineItemOutput());
            }
        }
        /// <summary>
        /// Prints an invoice for jobs ordered.
        /// </summary>
        public void PrintInvoice()
        {
            try
            {
                decimal invoiceTotal = 0;
                StringBuilder html = new StringBuilder("<!DOCTYPE html><html lang='en' xmlns='http://www.w3.org/1999/xhtml'>");

                html.Append("<head><meta charset='utf-8' /><title>InnerWorking Invoice</title></head>");
                html.Append("<body><table>");

                foreach (Job job in Jobs.OrderBy(o => o.Id))
                {
                    invoiceTotal += job.JobTotal;
                    html.Append($"<tr><td colspan='2'>Job Number { job.Id }</td></tr>");

                    foreach (PrintItem item in job.PrintItems)
                    {
                        html.Append($"<tr><td>{ item.Description }</td><td>{ item.DisplayTotal.ToString("C")}</td></tr>");
                    }

                    html.Append($"<tr><td colspan='2'>Total: <b>{ job.JobTotal.ToString("C") }</b></td></tr>");
                    html.Append("<tr><td colspan='2'>&nbsp;</td></tr>");
                }

                html.Append($"<tr><td colspan='2'>Invoice Total: <b>{invoiceTotal.ToString("C")}</b></td></tr>");
                html.Append("</table></body></html>");

                File.WriteAllText(rootFolder + "invoice.html", html.ToString());
                Console.WriteLine("Invoice file was created at " + rootFolder + "invoice.html" + Environment.NewLine);
            }
            catch (Exception WhatHappen)
            {
                Console.WriteLine("Exception occurred creating invoice.");
                Console.WriteLine($"Message: { WhatHappen.Message }");
            }

        }
        /// <summary>
        /// Accepts a csv file to get job that need to be processed. 
        /// </summary>
        public void ReadInputFile()
        {
            try
            {
                string[] AllLines = File.ReadAllLines(rootFolder + "BatchJobFile.csv");

                foreach (string line in AllLines.Skip(1))
                {
                    string[] tokens = line.Split('\t', StringSplitOptions.None);

                    PrintItem printItem = new PrintItem
                    {
                        JobId = Convert.ToInt32(tokens[0].ToString()),
                        Description = tokens[1].ToString().Trim(),
                        Cost = Convert.ToDecimal(tokens[2].ToString()),
                        ChargeExtraMargin = Convert.ToBoolean(Convert.ToInt32(tokens[3].ToString())),
                        ChargeSaleTax = Convert.ToBoolean(Convert.ToInt32(tokens[4].ToString()))
                    };

                    //Check if print item needs to be add a existing Job based on job id 
                    //or to a new one.
                    if (Jobs.Select(o => o.Id).ToList().Any(o => o == printItem.JobId))
                    {
                        Job job = Jobs.Where(o => o.Id == printItem.JobId).FirstOrDefault();
                        job.PrintItems.Add(printItem);
                    }
                    else
                    {
                        Jobs.Add(new Job(printItem));
                    }
                }
            }
            catch (Exception WhatHappen)
            {
                Console.WriteLine("Error reading input file.");
                Console.WriteLine($"The error message is { WhatHappen.Message }");
                Console.WriteLine("Please check the input file.");
                Console.WriteLine("We are looking for the input file 'BatchJobFile.csv' at " + rootFolder);
                Console.WriteLine("Retry application again.");
                Console.ReadLine();
            }

        }
    }
}
