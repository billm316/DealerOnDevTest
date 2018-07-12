using System;

namespace DealerOnTest
{
    public class ReceiptUI
    {
        private ShoppingCart _cart;

        public ReceiptUI()
        {
            Catalog.Instance().LoadCatalogTestData();
            _cart = new ShoppingCart();
        }

        public void ProcessOrder()
        {
            DisplayCatalogueItems();
            DisplayActions();
            GetMenuSelection();
        }

        private void AddItemToCart(string input)
        {
            long itemNum = 0;

            if (Int64.TryParse(input, out itemNum))
            {
                Item item = Catalog.Instance().GetItem(itemNum);
                if (item != null)
                {
                    int quantity = GetItemQuantity();
                    if (quantity > 0)
                    {
                        _cart.AddItem(item, quantity);
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid Item");
                }
            }
        }

        private void DisplayActions()
        {
            Console.WriteLine();
            Console.WriteLine("({0}) {1}", "P", "Print Receipt");
            Console.WriteLine("({0}) {1}", "C", "Display Catalogue");
            Console.WriteLine("({0}) {1}", "S", "Display Cart");
            Console.WriteLine("({0}) {1}", "D", "Delete Items in Cart");
            Console.WriteLine("({0}) {1}", "A", "Add Item to Catalog");
            Console.WriteLine("({0}) {1}", "Q", "Quit");
        }

        private void DisplayCatalogueItems()
        {
            foreach (Item i in Catalog.Instance().GetItems())
            {
                Console.WriteLine("{0} - {1} @ {2}", i.ItemId, i.Description, i.ListPrice);
            }
        }

        private void DisplayShoppingCartItems()
        {
            int itemCount = 0;
            foreach (LineItem i in _cart.GetItems())
            {
                Console.WriteLine("{2} {0} at {1}",
                    i.Item.Description,
                    i.Item.ListPrice,
                    i.Quantity);
                itemCount++;
            }
        }

        private int GetItemQuantity()
        {
            int quantity = 1;
            Console.WriteLine("Enter quantity or [Enter] for 1");
            string input = Console.ReadLine();
            if (input.Length > 0)
            {
                if (!Int32.TryParse(input, out quantity))
                {
                    Console.WriteLine("Not a valid quantity");
                    quantity = -1;

                }
            }
            return quantity;
        }

        private void GetMenuSelection()
        {
            bool quit = false;

            while (!quit)
            {
                string input = Console.ReadLine();
                input = input.ToUpper();
                switch (input)
                {
                    case "P":
                        // Print receipt
                        Console.WriteLine(Receipt.GetReceipt(_cart));
                        DisplayActions();
                        break;

                    case "C":
                        DisplayCatalogueItems();
                        DisplayActions();
                        break;

                    case "S":
                        DisplayShoppingCartItems();
                        DisplayActions();
                        break;

                    case "D":
                        _cart = new ShoppingCart();
                        DisplayShoppingCartItems();
                        DisplayActions();
                        break;

                    case "A":
                        GetNewItemValues();
                        break;

                    case "Q":
                        quit = true;
                        break;

                    default:
                        // Add item to shopping cart
                        AddItemToCart(input);
                        DisplayActions();
                        break;
                }
            }
        }

        private void GetNewItemValues()
        {
            Console.WriteLine("Enter description or [Enter] to cancel");
            string description = Console.ReadLine();
            if (description.Length == 0)
                return;

            Console.WriteLine("Enter list price or [Enter] to cancel");
            string listPriceStr = Console.ReadLine();
            decimal listPrice;
            if (listPriceStr.Length == 0)
                return;
            else
            {
                if (!decimal.TryParse(listPriceStr, out listPrice))
                {
                    Console.WriteLine("invalid number");
                    return;
                }
            }

            Console.WriteLine("Enter basic tax exemption [N]None [B]Book [F]Food [M]Medicine or [Enter] to cancel");
            string exemption = Console.ReadLine().ToUpper();
            if (exemption.Length == 0)
                return;
            else if (!exemption.Equals("N") && !exemption.Equals("B") && !exemption.Equals("F") && !exemption.Equals("M"))
            {
                Console.WriteLine("invalid selection");
                return;
            }

            Console.WriteLine("Import tariff? [Y] or [N] or [Enter] to cancel");
            string importTariff = Console.ReadLine().ToUpper();
            if (importTariff.Length == 0)
                return;
            else if (!importTariff.Equals("N") && !importTariff.Equals("Y"))
            {
                Console.WriteLine("invalid selection");
                return;
            }

            AddNewItemToCatalog(description, listPrice, exemption, importTariff);
        }

        private void AddNewItemToCatalog(
            string description,
            decimal listPrice,
            string basicExemption,
            string import)
        {
            SalesTax.BasicExempt exempt;
            switch (basicExemption)
            {
                case "B":
                    exempt = SalesTax.BasicExempt.Book;
                    break;
                case "F":
                    exempt = SalesTax.BasicExempt.Food;
                    break;
                case "M":
                    exempt = SalesTax.BasicExempt.Medicine;
                    break;
                default:
                    exempt = SalesTax.BasicExempt.None;
                    break;
            }

            Catalog.Instance().AddItem(new Item(description,
                decimal.Round(listPrice, 2),
                exempt,
                import.Equals("Y") ? true : false
            ));
        }
    }
}
