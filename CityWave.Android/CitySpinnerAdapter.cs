using Android.Content;
using Android.Views;
using Android.Widget;
using CityWave.Api;
using CityWave.Api.Types;

namespace CityWave.Android
{
    public class CitySpinnerAdapter : BaseAdapter<City>
    {
        private Context _context;
        private City[] _items;
        private Client _apiClient;
        
        public CitySpinnerAdapter(Context context)
        {
            _context = context;
            _apiClient = new Client(Preferences.Token);

            if (!TryLoadItemsFromCache())
                _items = new City[0];

            LoadItems();
        }

        private bool TryLoadItemsFromCache()
        {
            var cached = Preferences.Cities;

            if (cached != null)
            {
                _items = cached;
                NotifyDataSetChanged();

                return true;
            }

            return false;
        }

        private async void LoadItems()
        {
            await _apiClient.GetCities().Process(
                cities => _items = Preferences.Cities = cities,
                error => Toast.MakeText(_context, error as string, ToastLength.Long).Show()
            );

            NotifyDataSetChanged();
        }

        public override City this[int position]
            => _items[position];

        public override int Count
            => _items.Length;

        public override long GetItemId(int position)
            => _items[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.CitySpinnerItem, null, false);

            view.FindViewById<TextView>(Resource.Id.CityNameTextView).Text = _items[position].Name;

            return view;
        }
    }
}