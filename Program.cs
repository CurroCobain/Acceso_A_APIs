using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;


namespace AccederAApis
{
    // Clases que vamos a utilizar para gestionar la petición de datos a la api
    [Serializable]
    public class InfoDto
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string? next { get; set; }
        public string? prev { get; set; }
    }
    [Serializable]
    public class RespuestaDto
    {
        public InfoDto info { get; set; }
        public List<CharacterDto> results { get; set; }
    }
    [Serializable]
    public class CharacterDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public Location origin { get; set; }
        public Location location { get; set; }
        public string image { get; set; }
        public List<string> episode { get; set; }
        public string url { get; set; }
        public string created { get; set; }
        // Función para mostrar nombre y el total de episodios en los que aparece el personaje
        public string mostrar()
        {
            return "nombre del personaje ->" + (this.name ?? "N/A") + "\n" +
                   "episodios en los que aparece -> " + this.episode.Count;
        }
    }
    [Serializable]
    public class Location
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    class Program
    {
        static async Task Main()
        {
            // URL de la API
            string apiUrl = "https://rickandmortyapi.com/api/character/?name=Summer";
         
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Lanzamos la solicitud a la API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    //Si la solicitud fue correcta se gestiona el resultado
                    if (response.IsSuccessStatusCode)
                    {
                        // Tratamos el resultado como un String
                        string content = await response.Content.ReadAsStringAsync();
                        // Deserializamos el contenido de la respuesta de JSON a un objeto de tipo RespuestaDTO
                        RespuestaDto respuesta = JsonConvert.DeserializeObject<RespuestaDto>(content);
                        // Obtenemos el personaje que buscamos de la respuesta 
                        CharacterDto summer = respuesta.results[0];
                        // Imprimimos el resultado por consola
                        Console.WriteLine(summer.mostrar());
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

