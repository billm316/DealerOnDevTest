namespace DealerOnTest
{
    /// <summary>
    /// A class to describe the items available in the store.
    /// </summary>
    public class Item
    {
        public long ItemId { get; }
        public string Description { get; set; }
        public decimal ListPrice { get; }
        public decimal ItemSalesTax { get; }

        private static long _itemId = 0;

        public Item(
            string description,
            decimal listPrice,
            SalesTax.BasicExempt exemption,
            bool imported = false)
        {
            ItemId = _itemId++;
            Description = description;
            ListPrice = listPrice;
            ItemSalesTax = SalesTax.CalcTax(ListPrice, exemption, imported);
        }
    }
}
