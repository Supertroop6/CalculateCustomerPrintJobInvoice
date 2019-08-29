using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessCustomerPrintOrders
{
    /// <summary>
    /// a job ordered by a customer.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Set the base margin for a job.
        /// </summary>
        private readonly decimal baseMargin = 0.11M;
        /// <summary>
        /// Set the extra margin for a job.
        /// </summary>
        private readonly decimal extraMargin = 0.05M;
        /// <summary>
        /// Set the tax rate for a job.
        /// </summary>
        private readonly decimal salesTaxRate = 0.07M;
        /// <summary>
        /// Gets or sets the job id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the job total. 
        /// </summary>
        public decimal JobTotal { get; set; }
        /// <summary>
        /// A collection of print items for a job.
        /// </summary>
        public List<PrintItem> PrintItems { get; set; }

        public Job(PrintItem PrintItem)
        {
            PrintItems = new List<PrintItem>();
            JobTotal = 0;

            this.Id = PrintItem.JobId;
            PrintItems.Add(PrintItem);
        }
        /// <summary>
        /// Calculate the line item charge for all print items for a job.
        /// </summary>
        public void ProcessAllPrintItems()
        {
            foreach (PrintItem printItem in PrintItems)
            {
                printItem.CalculateLineItemCharge(baseMargin, extraMargin, salesTaxRate);
            }
        }
        /// <summary>
        /// Display job information to the desktop console.
        /// </summary>
        public string DisplayLineItemOutput()
        {
            StringBuilder output = new StringBuilder($"Job { Id }" + Environment.NewLine);

            foreach (PrintItem item in PrintItems)
            {
                JobTotal += item.LineItemTotal;
                output.Append($"{ item.Description}:\t\t\t { item.DisplayTotal.ToString("C") }" + Environment.NewLine);
            }

            output.Append($"Total:\t\t\t { JobTotal.ToString("C") }");

            return output.ToString() + Environment.NewLine + Environment.NewLine;
        }
    }
}
