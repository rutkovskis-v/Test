using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private readonly List<Product> _products;
        private Money _amount;

        public VendingMachine(string manufacturer, List<Product> products)
        {
            Manufacturer = manufacturer;
            if (string.IsNullOrEmpty(manufacturer))
                throw new InvalidNameException();
            _products = products;   
        }

        public string Manufacturer { get; }

        public bool HasProducts => _products.Any(p => p.Available > 0);

        public Money Amount => _amount;

        public Product[] Products => _products.ToArray();

        public bool AddProduct(string name, Money price, int count)
        {         
            if (string.IsNullOrEmpty(name))
                throw new InvalidNameException();
            if (count < 0)
                throw new NegativeAmountException();
            if (price.Euros < 0 || (price.Euros == 0 && price.Cents <= 0))
                throw new InvalidPriceException();

            Product newProduct = new Product { Name = name, Price = price, Available = count };
            _products.Add(newProduct);

            return true;
        }

        public Money InsertCoin(Money amount)
        {
            _amount = new Money { Euros = _amount.Euros + amount.Euros, Cents = _amount.Cents + amount.Cents };
            return new Money { Euros = 0, Cents = 0 };
        }

        public Money ReturnMoney()
        {
            Money returnedAmount = _amount;

            _amount = new Money { Euros = 0, Cents = 0 };
            return returnedAmount;
        }

        public bool UpdateProduct(int productNumber, string name, Money? price, int amount)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidNameException();
            if (amount < 0)
                throw new NegativeAmountException();
            if (price.HasValue)
            {
                if (price.Value.Euros < 0 || (price.Value.Euros == 0 && price.Value.Cents <= 0))
                    throw new InvalidPriceException();
            }

            Product product = _products[productNumber];
            _products[productNumber] = new Product { Name = name, Price = price.Value, Available = amount };

            product.Name = name;
            product.Price = price.Value;
            product.Available = amount;

            return true;
        }
    }
}

