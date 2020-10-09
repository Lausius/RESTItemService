using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ModelLib;
using ModelLib.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTItemService.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public static List<Item> items = new List<Item>()
            {
                new Item("Beer", "Not Good", 10),
                new Item("Bread", "Medium", 20),
                new Item("Cucumber", "Bad", 30),
                new Item("Carrot", "Low", 40),
                new Item("Beef", "Good", 50)
            };

        // GET: api/<ItemsController>
        /// <summary>
        /// api/Items returns all item data.
        /// </summary>
        /// <returns>items</returns>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET api/<ItemsController>/5
        /// <summary>
        /// Returns specific item by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item by ID</returns>
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return items.Find(i => i.Id == id);
        }

        // POST api/<ItemsController>
        /// <summary>
        /// Adds an item to the items list, if the correct values are used.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            value.Id = Item.counter;
            items.Add(value);
        }

        // PUT api/<ItemsController>/5
        /// <summary>
        /// Updates an item on the list by selecting ID value and afterwards inputting the new data. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        // DELETE api/<ItemsController>/5
        /// <summary>
        /// Deletes an item with selected ID.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            items.Remove(item);
        }

        // GET from string
        /// <summary>
        /// Returns a list item, where the name matches with the input string.
        /// </summary>
        /// <param name="subString"></param>
        /// <returns>list item</returns>
        [HttpGet]
        [Route("Name/{subString}")]
        public IEnumerable<Item> GetFromSubString(string subString)
        {
            return items.FindAll(i => i.Name.Contains(subString));
        }

        /// <summary>
        /// Returns a list item, where the quality matches with the input string.
        /// </summary>
        /// <param name="quality"></param>
        /// <returns>list item</returns>
        [HttpGet]
        [Route("Quality/{quality}")]
        public IEnumerable<Item> GetFromQuality(string quality)
        {
            return items.FindAll(i => i.Quality.Contains(quality));
        }

        /// <summary>
        /// Returns list items, where quantity is between two values (LowQuantity&HighQuantity).
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>list items between selected LowQuantity and HighQuantity</returns>
        [HttpGet]
        [Route("search")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItems filter)
        {
            return items.Where(i => i.Quantity >= filter.LowQuantity && i.Quantity <= filter.HighQuantity);
        }
    }
}
