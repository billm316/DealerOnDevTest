using Microsoft.VisualStudio.TestTools.UnitTesting;
using DealerOnTest;

namespace UnitTestProject
{
    [TestClass]
    public class ReceiptUnitTest
    {
        [TestMethod]
        public void GetReceipt()
        {
            //public static string GetReceipt(ShoppingCart sc)
            ShoppingCart sc = new ShoppingCart();

            // Test enpty shopping cart
            string receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Sales Taxes: 0.00\nTotal: 0.00\n", receipt, "Empty shopping cart");

            // Test one item / no sales tax / no tariff 
            Item item1 = new Item("Item One", 1.99m, SalesTax.BasicExempt.Book);
            sc.AddItem(item1, 1);
            receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Item One: 1.99\nSales Taxes: 0.00\nTotal: 1.99\n", receipt, "One item / no sales tax / no tariff ");

            // Test one item / no tariff
            sc = new ShoppingCart();
            item1 = new Item("Item One", 1.99m, SalesTax.BasicExempt.None);
            sc.AddItem(item1, 1);
            receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Item One: 2.19\nSales Taxes: 0.20\nTotal: 2.19\n", receipt, "One item / no tariff ");

            // Test one item w/sales tax w/tariff
            sc = new ShoppingCart();
            item1 = new Item("Item One", 1.99m, SalesTax.BasicExempt.None, true);
            sc.AddItem(item1, 1);
            receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Item One: 2.29\nSales Taxes: 0.30\nTotal: 2.29\n", receipt, "One item / no tariff ");

            // Test two consecutive identical items are combined 
            sc = new ShoppingCart();
            item1 = new Item("Item One", 1.99m, SalesTax.BasicExempt.None, true);
            sc.AddItem(item1, 1);
            sc.AddItem(item1, 1);
            receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Item One: 4.58 (2 @ 2.29)\nSales Taxes: 0.60\nTotal: 4.58\n", receipt, "One item / no tariff ");

            // Test 2 identical items separated by a third are not combined
            // Test two consecutive identical items are combined 
            sc = new ShoppingCart();
            item1 = new Item("Item One", 1.99m, SalesTax.BasicExempt.None, true);
            Item item2 = new Item("Item Two", 2.00m, SalesTax.BasicExempt.Food, false);
            sc.AddItem(item1, 1);
            sc.AddItem(item2, 1);
            sc.AddItem(item1, 1);
            receipt = Receipt.GetReceipt(sc);
            Assert.AreEqual("Item One: 2.29\nItem Two: 2.00\nItem One: 2.29\nSales Taxes: 0.60\nTotal: 6.58\n", receipt, "One item / no tariff ");
        }
    }
}
