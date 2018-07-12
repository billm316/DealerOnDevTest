using System.Collections.Generic;

namespace DealerOnTest
{
    public class ShoppingCart
    {
        private List<LineItem> _Items;

        public ShoppingCart()
        {
            _Items = new List<LineItem>();
        }

        /// <summary>
        /// Adds a LineItem for an Item and quantity to ShoppingCart.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public void AddItem(Item item, int quantity)
        {
            _Items.Add(new LineItem
            {
                Item = item,
                Quantity = quantity
            });
        }

        /// <summary>
        /// Returns the a List of LineItem contained by this ShoppingCart.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineItem> GetItems()
        {
            return new List<LineItem>(_Items);
        }
    }
}
