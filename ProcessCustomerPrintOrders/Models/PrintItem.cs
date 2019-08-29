using System;

namespace ProcessCustomerPrintOrders
{
    public class PrintItem

    {
        /// <summary>
        /// The job id this print item is associated with.
        /// </summary>
        public int JobId { get; set; }
        /// <summary>
        /// Get or set if this print item run should be charged an extra margin fee.
        /// </summary>
        public Boolean ChargeExtraMargin { get; set; }
        /// <summary>
        /// Get or set if the item should be taxed exempt or not.
        /// </summary>
        public Boolean ChargeSaleTax { get; set; }
        /// <summary>
        /// The print item description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The base cost of the print job.
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// The sale tax of the item.
        /// </summary>
        public decimal SalesTax { get; set; }
        /// <summary>
        /// The margin cost of the item.
        /// </summary>
        public decimal MarginCost { get; set; }
        /// <summary>
        /// Display total (Cost + SalesTax).
        /// </summary>
        public decimal DisplayTotal { get; set; }
        /// <summary>
        /// The line item total (Cost + SalesTax + MarginCost)
        /// </summary>
        public decimal LineItemTotal { get; set; }

        public PrintItem()
        {
            JobId = 0;
            ChargeExtraMargin = false;
            ChargeSaleTax = true;
            Description = string.Empty;
            Cost = 0;
            SalesTax = 0;
            MarginCost = 0;
            DisplayTotal = 0;
            LineItemTotal = 0;
        }

        /// <summary>
        /// Cal
        /// </summary>
        /// <param name="BaseMargin"></param>
        /// <param name="ExtraMargin"></param>
        /// <param name="SalesTaxRate"></param>
        public void CalculateLineItemCharge(decimal BaseMargin, decimal ExtraMargin, decimal SalesTaxRate)
        {
            if (ChargeExtraMargin)
            {
                MarginCost = Cost * (BaseMargin + ExtraMargin);
            }
            else
            {
                MarginCost = Cost * BaseMargin;
            }

            if (ChargeSaleTax)
            {
                SalesTax = Cost * SalesTaxRate;
            }

            DisplayTotal = Cost + SalesTax;
            LineItemTotal = Cost + SalesTax + MarginCost;
        }
    }
}
