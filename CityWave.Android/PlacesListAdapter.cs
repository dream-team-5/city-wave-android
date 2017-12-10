using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using CityWave.Api;
using CityWave.Api.Types;
using System;
using System.Threading.Tasks;

namespace CityWave.Android
{
    public class PlacesListAdapter : BaseAdapter<ShortPlace>
    {
        private Context _context;
        private ShortPlace[] _items;
        private string[] _images;
        private Client _apiClient;

        public event Action ItemsLoaded;
        public event Action ItemsLoading;

        public PlacesListAdapter(Context context)
        {
            _context = context;
            _items = new ShortPlace[0];
            _images = new string[0];
            _apiClient = new Client(Preferences.Token);
        }

        public async void LoadItems(long cityId, int? page = null)
        {
            ItemsLoading?.Invoke();

            await _apiClient.GetCityPlaces(1, page: page).Process(
                places =>
                {
                    _images = new string[places.Length];

                    Parallel.ForEach(places, async (place, _, index)
                        => _images[index] = await ImageStorage.StoreImage("places", place.Id, place.PhotoUrl)
                    );

                    _items = places;
                },
                error => Toast.MakeText(_context, error as string, ToastLength.Long).Show()
            );

            NotifyDataSetChanged();

            ItemsLoaded?.Invoke();
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
            view.FindViewById<TextView>(Resource.Id.PlaceAddressTextView).Text = _items[position].Address;
            view.FindViewById<ImageView>(Resource.Id.PlacePictureImageView).SetImageBitmap(BitmapFactory.DecodeFile(_images[position]));

            return view;
        }
    }
}