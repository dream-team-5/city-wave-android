using Android.App;
using Android.OS;

namespace CityWave.Android
{
    [Activity(Label = "PlacesActivity")]
    public class PlacesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Places);
        }
    }
}