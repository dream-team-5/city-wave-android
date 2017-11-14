using Android.Content;
using Android.Preferences;
using System;

namespace CityWave.Android
{
    public class Preferences : IDisposable
    {
        private ISharedPreferences _preferences;

        public Preferences(Context context)
            => _preferences = PreferenceManager.GetDefaultSharedPreferences(context);

        private const string TokenKey = "token";
        private string _token;
        public string Token
        {
            get => _token ?? (_token = _preferences.GetString(TokenKey, null));

            set
            {
                using (var editor = _preferences.Edit())
                {
                    if (value != null)
                        editor.PutString(TokenKey, value);
                    else
                        editor.Remove(TokenKey);

                    editor.Apply();
                }

                _token = value;
            }
        }

        public void Dispose() 
            => _preferences.Dispose();
    }
}