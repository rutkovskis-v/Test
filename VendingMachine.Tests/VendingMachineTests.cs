using FluentAssertions;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        private IVendingMachine _machine;
        private List<Product> _products;

        [TestInitialize]
        public void Setup()
        {
            _products = new List<Product>();
            _machine = new VendingMachine("test", _products);
        }

        [TestMethod]
        public void Vending_insertMoney()
        {
            var result = _machine.InsertCoin(new Money { Euros = 100, Cents = 50 });
            _machine.Amount.Euros.Should().Be(100);
            _machine.Amount.Cents.Should().Be(50);
        }

        [TestMethod]
        public void Vending_returnMoney()
        {
            var result = _machine.ReturnMoney();
            result.Cents.Should().Be(0);
            result.Euros.Should().Be(0);
        }
        [TestMethod]
        public void Vending_manufacturerWithWithEmptyName_ThrowsException()
        {
            Action action = () => new VendingMachine("", _products);
            action.Should().Throw<InvalidNameException>();
        }
        [TestMethod]
        public void AddProduct_addProductWithNamePriceAmount_ProductAdded()
        {
            var result = _machine.AddProduct("test", new Money { Euros = 2, Cents = 50 }, 1);
            result.Should().BeTrue();
            _products.Should().NotBeNull();
            _products.Should().Contain(p =>
                p.Name == "test" &&
                p.Price.Euros == 2 &&
                p.Price.Cents == 50 &&
                p.Available == 1);
        }

        [TestMethod]
        public void AddProduct_addProductWithEmptyName_ThrowsInvalidNameException()
        {
            Action action = () => _machine.AddProduct("", new Money { Euros = 2, Cents = 50 }, 1);

            action.Should().Throw<InvalidNameException>();
        }

        [TestMethod]
        public void AddProduct_addProductWitNegativeCount_ThrowsInvalidAmountException()
        {
            Action action = () => _machine.AddProduct("test", new Money { Euros = 2, Cents = 50 }, -1);

            action.Should().Throw<NegativeAmountException>();
        }

        [TestMethod]
        public void AddProduct_addProductWitNegativePrice_ThrowsInvalidPriceException()
        {
            Action action = () => _machine.AddProduct("test", new Money { Euros = -1, Cents = 0 }, 1);
            Action action1 = () => _machine.AddProduct("test", new Money { Euros = 0, Cents = -50 }, 1);

            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void AddProduct_addProductWitZeroPrice_ThrowsInvalidPriceException()
        {
            Action action = () => _machine.AddProduct("test", new Money { Euros = 0, Cents = 0 }, 1);

            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void Products_shouldReturnEmptyArrIfNoProductAdded()
        {
            var result = new VendingMachine("test", _products);

            var products = result.Products;

            products.Should().BeEmpty();
        }

        [TestMethod]
        public void UpdateProduct_updateProductWithNewNamePriceAmount_ProductUpdated()
        {
            var initialPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", initialPrice, 1);

            var updatedPrice = new Money { Euros = 3, Cents = 0 };

            var result = _machine.UpdateProduct(0, "updatedProduct", updatedPrice, 2);

            result.Should().BeTrue(); 

            var updatedProduct = _machine.Products[0];
            updatedProduct.Name.Should().Be("updatedProduct");
            updatedProduct.Price.Should().BeEquivalentTo(updatedPrice);
            updatedProduct.Available.Should().Be(2);
        }

        [TestMethod]
        public void UpdateProduct_updateProductWithEmptyName_ThrowsInvalidNameException()
        {
            var initialPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", initialPrice, 1);

            var updatedPrice = new Money { Euros = 3, Cents = 0 };

            Action action = () => _machine.UpdateProduct(0, "", updatedPrice, 1);

            action.Should().Throw<InvalidNameException>();
        }

        [TestMethod]
        public void UpdateProduct_updateProductWithNegativeAmount_ThrowsInvalidNameException()
        {
            var initialPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", initialPrice, 1);

            var updatedPrice = new Money { Euros = 3, Cents = 0 };

            Action action = () => _machine.UpdateProduct(0, "product", updatedPrice, -1);

            action.Should().Throw<NegativeAmountException>();
        }

        [TestMethod]
        public void UpdateProduct_UpdateProductWithNegativePrice_ThrowsInvalidPriceException()
        {
            var initialPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", initialPrice, 1);

            var updatedPrice = new Money { Euros = -3, Cents = -50 };

            Action action = () => _machine.UpdateProduct(0, "product", updatedPrice, 1);

            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void UpdateProduct_UpdateProductWithZeroPrice_ThrowsInvalidPriceException()
        {
            var initialPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", initialPrice, 1);

            var updatedPrice = new Money { Euros = 0, Cents = 0 };

            Action action = () => _machine.UpdateProduct(0, "product", updatedPrice, 1);

            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void HasProducts_TrueIfProductIsAvalible()
        {
            var productPrice = new Money { Euros = 2, Cents = 50 };

            _machine.AddProduct("product", productPrice, 1);

            _machine.HasProducts.Should().BeTrue(); 
        }

        [TestMethod]
        public void HasProducts_FalseIfProductIsAvalible()
        {
            _machine.HasProducts.Should().BeFalse(); 
        }

        [TestMethod]
        public void Manufacturer_ReturnsCorrectValue()
        {
            var manufacturer = _machine.Manufacturer;

            manufacturer.Should().Be("test"); 
        }
    }
}