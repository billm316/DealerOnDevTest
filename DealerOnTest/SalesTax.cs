using System;

namespace DealerOnTest
{
    /// <summary>
    /// Calculates a tax for an item rounded up to the nearest nickel.
    /// </summary>
    public class SalesTax
    {
        /// <summary>
        /// Provides values for the BasicExempt property of Item.
        /// </summary>
        public enum BasicExempt
        {
            None,
            Food,
            Medicine,
            Book
        }

        /// <summary>
        /// Calculates a tax for an item rounded up to the nearest nickel.
        /// </summary>
        /// <param name="price">The base price for calculating the tax.</param>
        /// <param name="basicTaxExemption">Indicates the exemption status of an Item.</param>
        /// <param name="imported">true indicates the Item is subject to import tariff.</param>
        /// <returns></returns>
        public static decimal CalcTax(
            decimal price,
            BasicExempt basicTaxExemption,
            bool imported = false)
        {
            decimal tax = BasicTax(price, basicTaxExemption);
            if (imported)
                tax += ImportTax(price);

            tax = RoundUpToNearestNickel(tax);

            return tax;
        }

        private static decimal[] roundTable = {
            .00m , .05m, .10m, .15m, .20m, .25m,
            .30m , .35m, .40m, .45m, .50m, .55m,
            .60m , .65m, .70m, .75m, .80m, .85m,
            .90m , .95m, 1.00m
        };

        private static decimal BasicTax(decimal price, BasicExempt basicTaxExemption)
        {
            decimal tax = 0m;

            switch(basicTaxExemption)
            {
                case SalesTax.BasicExempt.Book:
                case SalesTax.BasicExempt.Food:
                case SalesTax.BasicExempt.Medicine:
                    break;

                default:
                    tax = Decimal.Round(price * .10m, 2);
                    break;
            }
            return tax;
        }

        private static decimal ImportTax(decimal price) => Decimal.Round(price * .05m, 2);

        private static decimal RoundUpToNearestNickel(decimal tax)
        {
            decimal wholePart = Decimal.Truncate(tax);
            decimal decimalPart = tax - wholePart;

            for (int i = 0; i < roundTable.Length; i++)
            {
                if (decimalPart <= roundTable[i])
                {
                    decimalPart = roundTable[i];
                    break;
                }
            }

            return wholePart + decimalPart;
        }
    }
}