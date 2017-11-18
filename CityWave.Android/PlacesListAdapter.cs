using Android.Content;
using Android.Views;
using Android.Widget;
using CityWave.Api;
using CityWave.Api.Types;

namespace CityWave.Android
{
    public class PlacesListAdapter : BaseAdapter<ShortPlace>
    {
        private Context _context;
        private ShortPlace[] _items;
        private Client _apiClient;

        public PlacesListAdapter(Context context)
        {
            _context = context;
            _items = new ShortPlace[0];
            _apiClient = new Client(Preferences.Token);
        }

        public async void LoadItems(long cityId, int? page = null)
        {
            await _apiClient.GetCityPlaces(1, page: page).Process(
                places => _items = places,
                error => Toast.MakeText(_context, error as string, ToastLength.Long).Show()
            );

            NotifyDataSetChanged();
        }

        public override ShortPlace this[int position]
            => _items[position];

        public override int Count
            => _items.Length;

        public override long GetItemId(int position)
            => _items[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.PlacesListItem, null, false);

            view.FindViewById<TextView>(Resource.Id.PlaceNameTextView).Text = _items[position].Name;

            return view;
        }
    }
}