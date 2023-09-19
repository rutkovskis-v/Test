namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            IVendingMachine vendingMachine = new VendingMachine("Codelex", new List<Product>());

            vendingMachine.AddProduct("Cola", new Money { Euros = 2, Cents = 50 }, 10);
            vendingMachine.AddProduct("Snickers", new Money { Euros = 1, Cents = 75 }, 5);
            vendingMachine.AddProduct("Lays", new Money { Euros = 1, Cents = 30 }, 12);

            vendingMachine.InsertCoin(new Money { Euros = 3, Cents = 50 });

            foreach (Product product in vendingMachine.Products)
            {
                Console.WriteLine($"{product.Name} - Price: {product.Price.Euros}.{product.Price.Cents} Amount: {product.Available}");
            }

            Console.WriteLine($"Inserted coin amount: {vendingMachine.Amount.Euros}.{vendingMachine.Amount.Cents}");

            int selectedProduct = 0;
            Product purchase = vendingMachine.Products[selectedProduct];
            Console.WriteLine($"Selected product: {purchase.Name}");

            if (purchase.Price.Euros * 100 + purchase.Price.Cents <= vendingMachine.Amount.Euros * 100 + vendingMachine.Amount.Cents)
            {
                vendingMachine.InsertCoin(new Money { Euros = -purchase.Price.Euros, Cents = -purchase.Price.Cents });

                vendingMachine.UpdateProduct(selectedProduct, purchase.Name,
                    new Money { Euros = purchase.Price.Euros, Cents = purchase.Price.Cents }, purchase.Available - 1);

                Money change = vendingMachine.ReturnMoney();
                Console.WriteLine($"Change returned: {change.Euros}.{change.Cents}");
            }
            Console.WriteLine("\nUpdated product list: ");
            foreach (Product product in vendingMachine.Products)
            {
                Console.WriteLine($"{product.Name} - Price: {product.Price.Euros}.{product.Price.Cents} Amount: {product.Available}");
            }
        }
    }
}