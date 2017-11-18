using Android.App;
using Android.OS;

namespace CityWave.Android
{
    [Activity(Label = "City Wave", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(Preferences.Token == null ? typeof(LoginActivity) : typeof(HomeActivity));

            Finish();
        }
    }
}

