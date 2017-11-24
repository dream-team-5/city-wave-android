using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityWave.Android
{
    public static class ImageStorage
    {
        private const string _foldername = "CityWave";
        private static readonly string _path;

        static ImageStorage()
        {
            _path = Path.Combine(global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, _foldername);

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
                File.Create(Path.Combine(_path, ".nomedia")).Dispose();
            }
        }

        public static async Task<string> StoreImage(string prefix, long id, string imageUrl)
        {
            var filename = Path.Combine(_path, $"{ prefix }_{ id }");

            if (!File.Exists(filename) || (DateTime.Now - File.GetCreationTime(filename)).TotalDays >= 2)
                using (var httpClient = new HttpClient())
                using (var responseStream = await httpClient.GetStreamAsync(imageUrl))
                using (var imageStream = new FileStream(filename, FileMode.Create))
                    await responseStream.CopyToAsync(imageStream);

            return filename;
        }
    }
}
