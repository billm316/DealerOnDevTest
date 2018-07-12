using System.Collections.Generic;
using System.Text;

namespace DealerOnTest
{
    public class Receipt
    {
        public static string GetReceipt(ShoppingCart sc)
        {
            List<LineItem> lineItems = CreateLineItems(sc);
            return CreateReceipt(lineItems);
        }

        /// <summary>
        /// Combines consecutive line items where the items are identical. 
        /// Note the Item object itself must be the same, not just a clone.
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        private static List<LineItem> CreateLineItems(ShoppingCart sc)
        {
            List<LineItem> lineItems = new List<LineItem>();
            LineItem newLineItem = null;

            // Create LineItems
            foreach (LineItem scLineItems in sc.GetItems())
            {
                if (newLineItem == null)
                {
                    newLineItem = new LineItem { Item = scLineItems.Item, Quantity = scLineItems.Quantity };
                    lineItems.Add(newLineItem);
                }
                else if (newLineItem.Item == scLineItems.Item)
                {
                    newLineItem.Quantity += scLineItems.Quantity;
                }
                else
                {
                    newLineItem = new LineItem { Item = scLineItems.Item, Quantity = scLineItems.Quantity };
                    lineItems.Add(newLineItem);
                }
            }
            return lineItems;
        }

        private static string CreateReceipt(List<LineItem> lineItems)
        {
            StringBuilder sb = new StringBuilder();
            decimal totalSalesTax = 0;
            decimal totalSale = 0;

            foreach (LineItem li in lineItems)
            {
                decimal salesTaxes = li.Item.ItemSalesTax * li.Quantity;
                totalSalesTax += salesTaxes;
                decimal lineItemTotal = li.Item.ListPrice * li.Quantity + salesTaxes;
                totalSale += lineItemTotal;

                sb.Append(li.Item.Description)
                    .Append(": ")
                    .Append(string.Format("{0:0.00}", (li.Item.ListPrice + li.Item.ItemSalesTax) * li.Quantity));

                if (li.Quantity > 1)
                {
                    sb.Append(" (")
                        .Append(li.Quantity.ToString())
                        .Append(" @ ")
                        .Append(string.Format("{0:0.00}", (li.Item.ListPrice + li.Item.ItemSalesTax).ToString()))
                        .Append(")")
                        .ToString();
                }
                sb.Append("\n");
            }

            sb.Append("Sales Taxes: ").Append(string.Format("{0:0.00}", totalSalesTax)).Append("\n");
            sb.Append("Total: ").Append(string.Format("{0:0.00}", totalSale)).Append("\n");

            return sb.ToString();
        }
    }
}
