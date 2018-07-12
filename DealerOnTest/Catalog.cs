using System.Collections.Generic;

namespace DealerOnTest
{
    public class Catalog
    {
        private Dictionary<long, Item> _items;
        private static Catalog _catalog = null;

        public static Catalog Instance()
        {
            if(_catalog == null)
            {
                _catalog = new Catalog();
            }
            return _catalog;
        }

        private Catalog()
        {
            _items = new Dictionary<long, Item>();
        }

        /// <summary>
        /// Adds an item to the Catelogue.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>False if an item with same description already exist, otherwise true</returns>
        public void AddItem(Item item)
        {
            _items.Add(item.ItemId, item);
        }

        /// <summary>
        /// Returns the item if the itemKey matches otherwise null.
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public Item GetItem(long itemKey)
        {
            Item item = null;

            if(_items.ContainsKey(itemKey))
            {
                item = _items[itemKey];
            }

            return item;
        }

        /// <summary>
        /// Returns all Itmes in the catalog as a List of Item
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            return new List<Item>( _items.Values);
        }

        /// <summary>
        /// Load the catalog with test data.
        /// </summary>
        public void LoadCatalogTestData()
        {
            Item item = new Item("Book", 12.49m, SalesTax.BasicExempt.Book);
            AddItem(item);

            item = new Item("Music CD", 14.99m, SalesTax.BasicExempt.None);
            AddItem(item);

            item = new Item("Chocolate bar", .85m, SalesTax.BasicExempt.Food);
            AddItem(item);

            item = new Item("Imported box of chocolates", 10.00m, SalesTax.BasicExempt.Food, true);
            AddItem(item);

            item = new Item("Imported bottle of perfume", 47.50m, SalesTax.BasicExempt.None,true);
            AddItem(item);

            item = new Item("Bottle of perfume", 18.99m, SalesTax.BasicExempt.None);
            AddItem(item);

            item = new Item("Packet of headache pills", 9.75m, SalesTax.BasicExempt.Medicine);
            AddItem(item);

            item = new Item("Imported bottle of perfume", 27.99m, SalesTax.BasicExempt.None, true);
            AddItem(item);

            item = new Item("Imported box of chocolates", 11.25m, SalesTax.BasicExempt.Food, true);
            AddItem(item);
        }
    }
}
