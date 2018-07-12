/// <summary>
/// Design Decisions / Assumptions
///     A console ui was created to facilitate the creation of shopping cart.
///     
///     The print reecipt combines the same items into one line only when they are consecutive items in the shopping cart.
///     
///     Calculation of sales tax is by individual item regardless if they are combined into one line item on the receipt.
///     
///     Calculation of sales tax when combined with import duty is combined before being rounded.
///     
///     Class ReceiptUI - provides a console interface for creating receipts.
///     
///     Class Catalog - primary repository for all items in the system.
///     
///     Class Receipt - provides method top create a receipt string from a ShoppingCart object.
///     
///     Class Item -  describe the items available in the store.
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
