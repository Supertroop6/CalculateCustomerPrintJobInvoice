using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessCustomerPrintOrders;

namespace TestBatchApplication
{
    [TestClass]
    public class BatchTest
    {
        /// <summary>
        /// Base margin is 11%. Test base margin.
        /// </summary>
        [TestMethod]
        public void ValidateJobCalculationsValidateStandardMargin()
        {
            //Arrange
            PrintItem item = new PrintItem();
            Job job = new Job(item);

            item.JobId = 0;
            item.ChargeSaleTax = true;
            item.ChargeExtraMargin = false;
            item.Cost = 100;

            //Act
            job.ProcessAllPrintItems();

            //Assert margin cost is $11 for a $100
            Assert.AreEqual(item.MarginCost, 11);
        }
        /// <summary>
        /// Extra margin is 16%. Test extra margin.
        /// </summary>
        [TestMethod]
        public void ValidateJobCalculationsValidateExtraMargin()
        {
            //Arrange
            PrintItem item = new PrintItem();
            Job job = new Job(item);

            item.JobId = 0;
            item.ChargeSaleTax = true;
            item.ChargeExtraMargin = true;
            item.Cost = 100;

            //Act
            job.ProcessAllPrintItems();

            //Assert extra margin cost is $16 for a $100 item.
            Assert.AreEqual(item.MarginCost, 16);
        }
        /// <summary>
        /// Sales Tax rate is 7%. Test sales tax rate.
        /// </summary>
        [TestMethod]
        public void ValidatePrintItemStandardTax()
        {
            //Arrange
            PrintItem item = new PrintItem();
            Job job = new Job(item);

            item.JobId = 0;
            item.ChargeSaleTax = true;
            item.ChargeExtraMargin = true;
            item.Cost = 100;

            //Act
            job.ProcessAllPrintItems();

            //Assert sales tax is $7 for a $100 item.
            Assert.AreEqual(item.SalesTax, 7);
        }
        /// <summary>
        /// If Tax exempted job. Then total tax amount should be $0. Test exempted tax status.
        /// </summary>
        [TestMethod]
        public void ValidatePrintItemExemptTax()
        {
            //Arrange
            PrintItem item = new PrintItem();
            Job job = new Job(item);

            item.JobId = 0;
            item.ChargeSaleTax = false;
            item.ChargeExtraMargin = true;
            item.Cost = 100;

            //Act
            job.ProcessAllPrintItems();

            //Assert sale tax is 0 when ChargeSaleTax is false.
            Assert.AreEqual(item.SalesTax, 0);
        }
    }
}