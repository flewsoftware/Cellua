using System;
using Newtonsoft.Json;

namespace Cellua.Core.PackageManager
{
    public class Config
    {
        [JsonRequired]
        public string Main;

        public string[] ModulePaths;
    }
    public class PackageManager
    {
        public Config Config = new();
        
        public void LoadConfiguration(string config)
        {
            Config = JsonConvert.DeserializeObject<Config>(config) ?? throw new JsonSerializationException("Cant deserialize config file.");
        }

        public void LoadFromFile(string location)
        {
            LoadConfiguration(System.IO.File.ReadAllText(location));
        }
    }
}