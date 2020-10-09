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
            var list = await GetAllItemsAsync();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Enter ID:");
            int id;
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(await GetItemById(id));
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

        public async Task<Item> UpdateItem(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                Item newItem = new Item("kanyle", "heroin", 100);
                string jsonStr = JsonConvert.SerializeObject(newItem);
                StringContent stringContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                await client.PutAsync($"{URI}/{id}", stringContent);
                return newItem;

            }
        }

        public async Task<Item> DeleteItem(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync($"{URI}/{id}");
                await client.DeleteAsync($"{URI}/{id}");
                return JsonConvert.DeserializeObject<Item>(content);
            }
        }

        public async Task<Item> AddItem()
        {
            using (HttpClient client = new HttpClient())
            {
                Item newItem = new Item("ny ting", "lort", 32);
                string jsonStr = JsonConvert.SerializeObject(newItem);
                StringContent stringContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                await client.PostAsync(URI, stringContent);
                return newItem;
            }
        }
    }
}
