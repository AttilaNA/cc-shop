using System.IO;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Helpers
{
    public static class JsonHelper
    {
        public static void AppendJsonFile<T>(T data, string fileName)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.AppendAllText(json, $"./Data/${fileName}.json");
        }

        public static T ReadJsonFile<T>(string fileName)
        {
            string json = File.ReadAllText($"./Data/${fileName}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
