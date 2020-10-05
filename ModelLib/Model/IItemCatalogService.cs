using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public interface IItemCatalogService
    {
        void Add(Item newItem);
        IEnumerable<Item> GetAllItems();
        Item GetById(int id);
        void Remove(int id);
        void Update(int id, Item value);
        IEnumerable<Item> GetFromSubstring(string subString);
        IEnumerable<Item> GetFromQuality(string quality);
        IEnumerable<Item> GetFromQuantity(FilterItems filter);
    }
}
