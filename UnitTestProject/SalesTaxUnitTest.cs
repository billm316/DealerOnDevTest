using Microsoft.VisualStudio.TestTools.UnitTesting;
using DealerOnTest;

namespace UnitTestProject
{
    [TestClass]
    public class SalesTaxUnitTest
    {
        [TestMethod]
        public void CalcTax_BasicTaxTest()
        {
            //Basic sales tax applies to all items at a rate of 10% of the item’s list price, with the exception of books, food, and medical products.

            // Test no rounding
            decimal tax = SalesTax.CalcTax(50.00m, SalesTax.BasicExempt.None, false);
            Assert.AreEqual(5.00m, tax, "No rounding");

            // Test round up
            tax = SalesTax.CalcTax(19.90m, SalesTax.BasicExempt.None, false);
            Assert.AreEqual(2.00m, tax, "Round up");
        }

        [TestMethod]
        public void CalcTax_ExceptionsTest()
        {
            //Basic sales tax applies to all items at a rate of 10% of the item’s list price, with the exception of books, food, and medical products.

            decimal tax = SalesTax.CalcTax(5.60m, SalesTax.BasicExempt.Book, false);
            Assert.AreEqual(0.0m, tax, "Exception - Book Test");

            tax = SalesTax.CalcTax(5.60m, SalesTax.BasicExempt.Food, false);
            Assert.AreEqual(0.0m, tax, "Exception - Food  Test");

            tax = SalesTax.CalcTax(5.60m, SalesTax.BasicExempt.Medicine, false);
            Assert.AreEqual(0.0m, tax, "Exception - Medicine  Test");
        }

        [TestMethod]
        public void CalcTax_ImportDutyTest()
        {
            // import duty(import tax) applies to all imported items at a rate of 5 % of the shelf price, with no exceptions.

            // Test no rounding
            decimal tax = SalesTax.CalcTax(50.00m, SalesTax.BasicExempt.Food, true);
            Assert.AreEqual(2.50m, tax, "Only import: No rounding");

            // Test round up
            tax = SalesTax.CalcTax(19.90m, SalesTax.BasicExempt.Food, true);
            Assert.AreEqual(1.00m, tax, "Only import: Round up");

            // Test no rounding
            tax = SalesTax.CalcTax(50.00m, SalesTax.BasicExempt.None, true);
            Assert.AreEqual(7.50m, tax, "Basic + Import: No rounding");

            // Test round up
            tax = SalesTax.CalcTax(19.90m, SalesTax.BasicExempt.None, true);
            Assert.AreEqual(3.00m, tax, "Basic + Import: Round up");    
        }
    }
}
