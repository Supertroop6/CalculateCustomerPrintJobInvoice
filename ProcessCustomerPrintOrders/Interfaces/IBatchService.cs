using System.Collections.Generic;

namespace ProcessCustomerPrintOrders
{
    /// <summary>
    /// Interface outlining methods implementing IBatchService
    /// </summary>
    public interface IBatchService
    {

        /// <summary>
        /// A list of jobs to be run.
        /// </summary>
        List<Job> Jobs { get; set; }
        /// <summary>
        /// Create an html file to view invoice.
        /// </summary>
        void PrintInvoice();
        /// <summary>
        /// Reads a file to process a batch job.
        /// </summary>
        void ReadInputFile();
    }
}
