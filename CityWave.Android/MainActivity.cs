using Android.App;
using Android.OS;
using Android.Views;

namespace CityWave.Android
{
    [Activity(Label = "City Wave", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.LogIn);
        }
    }
}

