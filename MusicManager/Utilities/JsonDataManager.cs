using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.IO;
using System.Data.SqlTypes;

namespace MusicManager.Utilities
{
    enum SaveDataType
    {
        Query,
        Settings,
    }

    internal static class JsonDataManager
    {
        static public readonly Dictionary<SaveDataType, string> DefaultSavePathDict = new()
        {
            [SaveDataType.Query] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "queriesData.json"),
            [SaveDataType.Settings] = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json"),
        };

        static private JsonSerializerOptions _options;

        static JsonDataManager()
        {
            _options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
        }

        public static void SetSerializerOptions(JsonSerializerOptions options)
        {
            _options = options;
        }

        public static void SaveDataToJson(object data, string path)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(data, _options);

                string targetDirectory = Path.GetDirectoryName(path);

                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                File.WriteAllText(path, jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(nameof(JsonDataManager) + ex.Message);
            }
        }

        public static Type LoadObjectFromJson<Type>(string sourcePath) where Type : class
        {
            try
            {
                Type loadedObject = null;

                if (File.Exists(sourcePath))
                {
                    string jsonContent = File.ReadAllText(sourcePath);
                    loadedObject = JsonSerializer.Deserialize<Type>(jsonContent, _options);
                }

                return loadedObject;
            }
            catch (Exception ex)
            {
                MessageBox.Show(nameof(JsonDataManager) + ex.Message);
                return null;
            }
        }
    }
}
