using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TodoApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Text;

namespace TodoApp.Services
{
    public class ApiTodo
    {
        private const string BaseUrl = "http://toodoapp.azurewebsites.net/api/";
        public async Task<List<Todo>> GetTagsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}todo").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Todo>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        public async void SalvarTodo(Todo todo)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(todo).ToString(), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{BaseUrl}todo", httpContent).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        var content = JsonConvert.DeserializeObject<Todo>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        
                    }
                }
            }

        }

        private static void EditarTodo(string id)
        {
            Todo _todo = new Todo();
            using (var client = new HttpClient())
            {
                var responseGet = client.GetAsync($"{BaseUrl}todo/{id}").Result;

                if (responseGet.IsSuccessStatusCode)
                {
                    using (var responseStream = responseGet.Content.ReadAsStreamAsync())
                    {
                        _todo = JsonConvert.DeserializeObject<Todo>(responseGet.Content.ReadAsStringAsync().Result);
                        Console.WriteLine("Id: " + _todo.id + "\nTitulo: " + _todo.titulo + "\nFeito? " + _todo.feito);
                    }
                }

                Console.WriteLine("\n Edicao de todo:\n");
                string titulo = Console.ReadLine();
                _todo.titulo = titulo;

                var httpContent = new StringContent(JsonConvert.SerializeObject(_todo).ToString(), Encoding.UTF8, "application/json");

                var response = client.PutAsync($"{BaseUrl}todo", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Todo Editado com sucesso.");
                }
                else
                    Console.Write("Erro ao editar todo.");
            }
        }

        private static void ExcluirTodo(string id)
        {
            var httpClient = new HttpClient();

            var response = httpClient.DeleteAsync($"{BaseUrl}todo/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nFoi deletado o Todo com Id " + id + " \n");
            }
            else
            {
                Console.WriteLine("\n Não Foi deletado o Todo com Id " + id + " \n");
            }
        }

    }
}
