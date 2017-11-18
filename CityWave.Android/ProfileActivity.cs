using Android.App;
using Android.OS;

namespace CityWave.Android
{
    [Activity(Label = "Profile")]
    public class ProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Profile);
        }
    }
}