using Android.Content;
using Android.Views;
using Android.Widget;
using CityWave.Api;
using CityWave.Api.Types;
using System;

namespace CityWave.Android
{
    public class CategoriesListAdapter : BaseAdapter<Category>
    {
        private Context _context;
        private Category[] _items;
        private Client _apiClient;

        public event Action ItemsLoaded;
        public event Action ItemsLoading;

        public CategoriesListAdapter(Context context)
        {
            _context = context;
            _items = new Category[0];
            _apiClient = new Client(Preferences.Token);
        }

        public async void LoadItems()
        {
            ItemsLoading?.Invoke();

            await _apiClient.GetCategories().Process(
                categories => _items = categories,
                error => Toast.MakeText(_context, error as string, ToastLength.Long).Show()
            );

            NotifyDataSetChanged();

            ItemsLoaded?.Invoke();
        }

        public override Category this[int position]
            => _items[position];

        public override int Count
            => _items.Length;

        public override long GetItemId(int position)
            => _items[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.CategoriesListItem, null, false);

            view.FindViewById<TextView>(Resource.Id.CategoryNameTextView).Text = _items[position].Name;

            return view;
        }
    }
}