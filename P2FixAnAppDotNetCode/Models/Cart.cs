using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        List<CartLine> cartLineList = new List<CartLine>();

        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public List<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        public List<CartLine> GetCartLineList()
        {
            return cartLineList;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            CartLine newProduct = new CartLine(product, quantity);

            CartLine line = cartLineList.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                cartLineList.Add(newProduct);
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            List<double> productAmountList = new List<double>();

            foreach (CartLine cartline in cartLineList)
            {
                double quantity = Convert.ToDouble(cartline.Quantity);
                double price = cartline.Product.Price;
                double productAmount = quantity *= price;

                productAmountList.Add(productAmount);
            }

            return productAmountList.Sum();
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            double quantitySum = Convert.ToDouble(cartLineList.Sum(l => l.Quantity));

            return GetTotalValue() / quantitySum;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            CartLine specificCartLine = GetCartLineList().Find(cl => cl.Product.Id == productId);
            return specificCartLine.Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public CartLine(
            Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
