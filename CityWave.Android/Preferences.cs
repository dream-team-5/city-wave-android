using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CityWave.Android
{
    public static class Preferences
    {
        private const string _filename = "preferences.json";

        private static readonly string _path;
        private static Dictionary<string, object> _preferences;

        static Preferences()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _filename);

            if (File.Exists(_path))
                using (var reader = new StreamReader(_path))
                    _preferences = JsonConvert.DeserializeObject<Dictionary<string, object>>(reader.ReadToEnd());
            else
            {
                _preferences = new Dictionary<string, object>();

                using (var writer = new StreamWriter(_path))
                    writer.Write(JsonConvert.SerializeObject(_preferences));
            }
        }

        private static void Write(string key, object value)
        {
            _preferences[key] = value;

            using (var writer = new StreamWriter(_path))
                writer.Write(JsonConvert.SerializeObject(_preferences));
        }

        private const string TokenKey = "token";
        public static string Token
        {
            get => _preferences.GetValueOrDefault(TokenKey) as string;
            set
            {
                Write(TokenKey, value);

                TokenChanged?.Invoke(Token);
            }
        }
        public static event Action<string> TokenChanged;

        private const string CityIdKey = "city_id";
        public static long? CityId
        {
            get => _preferences.GetValueOrDefault(CityIdKey) as long?;
            set
            {
                Write(CityIdKey, value);

                CityIdChanged?.Invoke(CityId);
            }
        }
        public static event Action<long?> CityIdChanged;
    }
}