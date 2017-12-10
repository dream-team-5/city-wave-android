using Android.App;
using Android.OS;
using Android.Widget;

namespace CityWave.Android
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);

            FindViewById<Button>(Resource.Id.LogoutButton).Click += LogoutButton_Click;
        }

        private void LogoutButton_Click(object sender, System.EventArgs e)
        {
            Preferences.Clear();

            StartActivity(typeof(LoginActivity));

            Finish();
        }
    }
}