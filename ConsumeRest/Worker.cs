using ModelLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeRest
{
    class Worker
    {
        string URI = "http://restfulitemservice.azurewebsites.net/api/items";

        public async void Start()
        {
            //var list = await GetAllItemsAsync();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine(string.Join("\n", GetAllItemsAsync().Result));

            Console.WriteLine("Enter ID:");
            int id;
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(await GetItemById(id));

            Console.WriteLine("Updating item:");
            await UpdateItem(id);
            Console.WriteLine(await GetItemById(id));

            Console.WriteLine("Adding item:");
            await AddItem();
            Console.WriteLine(await GetItemById(6));

            Console.WriteLine("Deleting new item:");
            await DeleteItem(6);
            Console.WriteLine(string.Join("\n", GetAllItemsAsync().Result));


        }

        public async Task<IList<Item>> GetAllItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Item> cList = JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }

        public async Task<Item> GetItemById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync($"{URI}/{id}");
                Item item = JsonConvert.DeserializeObject<Item>(content);
                return item;
            }
        }

        public async Task UpdateItem(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                Item item = await GetItemById(id);
                item.Name = "ole";
                item.Quality = "fader";
                item.Quantity = 100;
                string jsonStr = JsonConvert.SerializeObject(item);
                StringContent stringContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                await client.PutAsync($"{URI}/{id}", stringContent);

            }
        }

        public async Task DeleteItem(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync($"{URI}/{id}");
            }
        }

        public async Task AddItem()
        {
            using (HttpClient client = new HttpClient())
            {
                Item newItem = new Item("nyere ting", "oi", 499);
                string jsonStr = JsonConvert.SerializeObject(newItem);
                StringContent stringContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                await client.PostAsync(URI, stringContent);
            }
        }
    }
}
