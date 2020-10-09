using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLib.Model
{
    public class ItemCatalogService : IItemCatalogService
    {
        public List<Item> _itemList = new List<Item>()
            {
                new Item( "Beer", "Not Good", 10),
                new Item( "Bread", "Medium", 20),
                new Item( "Cucumber", "Bad", 30),
                new Item( "Carrot", "Low", 40),
                new Item( "Beef", "Good", 50)
            };

        public void Add(Item newItem)
        {
            _itemList.Add(newItem);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemList;
        }

        public Item GetById(int id)
        {
            return _itemList.Find(i => i.Id == id);
        }

        public IEnumerable<Item> GetFromQuality(string quality)
        {
            return _itemList.FindAll(i => i.Quality.Contains(quality));
        }

        public IEnumerable<Item> GetFromQuantity(FilterItems filter)
        {
            return _itemList.Where(i => i.Quantity >= filter.LowQuantity && i.Quantity <= filter.HighQuantity);
        }

        public IEnumerable<Item> GetFromSubstring(string subString)
        {
            return _itemList.FindAll(i => i.Name.Contains(subString));
        }

        public void Update(int id, Item value)
        {
            Item item = GetById(id);
            if (item != null)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        public void Remove(int id)
        {
            Item item = GetById(id);
            _itemList.Remove(item);
        }
    }
}
