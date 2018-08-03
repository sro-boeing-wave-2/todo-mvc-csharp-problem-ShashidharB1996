using Microsoft.VisualStudio.TestTools.UnitTesting;
using keep.Models;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new ProductController(testProducts);

            var result = controller.GetByLabel() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}
