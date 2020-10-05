using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLib;
using ModelLib.Model;
using RESTItemService.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace ItemsControllerUnitTests
{
    [TestClass]
    public class ItemsControllerTest
    {
        private ItemsController controller;
        private IItemCatalogService service;
        private List<Item> mockItems;

        [TestInitialize]
        public void SetUpItems()
        {
            mockItems = new List<Item>();
            //mockItems.Add(new Item { Id = 1, Name = "Demo1", Quality = "bad", Quantity = 1 });
            //mockItems.Add(new Item { Id = 2, Name = "Demo2", Quality = "good", Quantity = 3.75 });
            //mockItems.Add(new Item { Id = 3, Name = "Demo3", Quality = "ugly", Quantity = 16.99 });
            //mockItems.Add(new Item { Id = 4, Name = "Demo4", Quality = "low", Quantity = 11.00 });
            service = new ItemCatalogService();
            controller = new ItemsController(service);
            mockItems = (List<Item>)service.GetAllItems();
        }

        [TestMethod]
        public void GetAllItemsTest()
        {
            var result = controller.Get() as List<Item>;
            Assert.AreEqual(mockItems.Count, result.Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {

            Item result = controller.Get(4);
            Assert.AreEqual(mockItems[3].Id, result.Id);
        }

        [TestMethod]
        public void PostTest()
        {
            Item newItem = new Item(5, "fisk", "Shite", 200);
            mockItems.Add(newItem);

            controller.Post(newItem);
            var result = controller.Get() as List<Item>;
            Assert.AreEqual(mockItems.Count, result.Count);
        }

        [TestMethod]
        public void DeleteTest()
        {
            mockItems.RemoveAt(0);

            controller.Delete(1);
            var result = controller.Get() as List<Item>;
            Assert.AreEqual(mockItems.Count, result.Count);
        }

        [TestMethod]
        public void PutTest()
        {
            Item updatedValues = new Item(10, "fisk", "not good", 400);
            mockItems[0] = updatedValues;

            controller.Put(1, updatedValues);
            var result = controller.Get(10);
            Assert.AreEqual(mockItems[0], result);
        }


        [TestMethod]
        public void GetFromSubstringTest()
        {
            var result = controller.GetFromSubString("Demo") as List<Item>;

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void GetFromQualityTest()
        {
            var result = controller.GetFromQuality("ugly") as List<Item>;

            Assert.AreEqual(1, result.Count);

        }

        //[TestMethod]
        //public void GetWithFilterTest()
        //{
        //    FilterItems filterItem = new FilterItems
        //    {
        //        LowQuantity = 1,
        //        HighQuantity = 11
        //    };

        //    var testItems = GetTestItems();
        //    var controller = new ItemsController(testItems);
        //    var result = controller.GetWithFilter(filterItem) as List<Item>;

        //    Assert.AreEqual(3, result.Count);
        //}
    }
}
