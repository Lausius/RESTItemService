using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTItemService.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using ModelLib.Model;

namespace ItemsControllerTest
{
    [TestClass]
    class ItemsControllerTest
    {
        ItemsController itemsController = new ItemsController();

        [TestMethod]
        public void TestGet()
        {
            var testList = itemsController.Get();
            Assert.AreEqual(testList, Item.items);
        }
    }
}
