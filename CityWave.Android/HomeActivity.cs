using Android.App;
using Android.OS;
using Android.Widget;

namespace CityWave.Android
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);

            FindViewById<Button>(Resource.Id.ResetButton).Click += (s, e) =>
            {
                using (var prefs = new Preferences(this))
                    prefs.Token = null;

                StartActivity(typeof(LoginActivity));

                Finish();
            };

            using (var prefs = new Preferences(this))
                FindViewById<TextView>(Resource.Id.TokenTextView).Text = prefs.Token;
        }
    }
}