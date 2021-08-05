using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Async_HW_1
{
    public class PostReader
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task reader(int id)
        {
            var response = await _client.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}");         
            Console.WriteLine(response);
            await using StreamWriter writer = new StreamWriter("result.txt", true);
            await writer.WriteLineAsync(response);
            await writer.WriteLineAsync();
        }

        public async Task GetPosts(int from, int to)
        {
            for (int i = from; i <= to; i++)
            {                
                try
                {
                   await reader(i);
                }
                catch(AggregateException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }               
            }
        }
    }
    class Program
    {    
        static async Task Main(string[] args)
        {
            PostReader get = new PostReader();
            await get.GetPosts(4, 13);
            Console.ReadKey();
        }
    }
}
