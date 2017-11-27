using Android.App;
using Android.OS;
using Android.Widget;

namespace CityWave.Android
{
    [Activity(Label = "PlacesActivity")]
    public class PlacesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Places);

            var placesList = FindViewById<ListView>(Resource.Id.PlacesListView);
            var adapter = new PlacesListAdapter(this);

            placesList.Adapter = adapter;

            adapter.ItemsLoading += ()
                => FindViewById<ProgressBar>(Resource.Id.PlacesProgressBar).Visibility = global::Android.Views.ViewStates.Visible;

            adapter.ItemsLoaded += ()
                => FindViewById<ProgressBar>(Resource.Id.PlacesProgressBar).Visibility = global::Android.Views.ViewStates.Invisible;

            Preferences.CityIdChanged += cityId =>
            {
                if (cityId.HasValue)
                    adapter.LoadItems(cityId.Value);
            };
        }
    }
}