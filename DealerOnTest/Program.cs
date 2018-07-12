/// <summary>
/// DESIGN SUMMARY - PROBLEM TWO - SALES TAXES
/// The ReceiptUI object provides a console interface for:
///  - Displaying and adding items to a master catalog.
///  - Displaying, adding items to a shopping cart, and resetting the shopping cart.
///  - Printing a receipt to console.
///  
/// A Catalog object maintains a list of available Items that may be included in Receipt.
/// 
/// A ShoppingCart object maintains a list of Items to be included in a receipt.
/// 
/// The Receipt class provides method top create a receipt string from a ShoppingCart object.
/// 
/// The SalesTax class provides tax calculation for an Item based on its price, exemptions and 
/// imports duties.
/// 
/// The Item class describes an item available in the store.
/// 
/// DESIGN DECISIONS / ASSUMPTIONS
///     A console ui was created to facilitate the creation of shopping cart.
///     
///     The print receipt combines the same items into one line only when they are consecutive
///     items in the shopping cart.
///     
///     Calculation of sales tax is by individual item regardless if they are combined into one 
///     line item on the receipt.
///     
///     Calculation of sales tax when combined with import duty is combined before being rounded
///     to the nearest nickel.
/// </summary>
namespace DealerOnTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiptUI ui = new ReceiptUI();
            ui.ProcessOrder();
        }
    }
}
