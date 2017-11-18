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
            placesList.Adapter = new PlacesListAdapter(this);

            Preferences.CityIdChanged += cityId =>
            {
                if (cityId.HasValue)
                    ((PlacesListAdapter)placesList.Adapter).LoadItems(cityId.Value);
            };
        }
    }
}