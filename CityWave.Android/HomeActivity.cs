using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace CityWave.Android
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : TabActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);

            var citySpinner = FindViewById<Spinner>(Resource.Id.CitySpinner);
            citySpinner.Adapter = new CitySpinnerAdapter(this);
            citySpinner.ItemSelected += (s, e) => Preferences.CityId = e.Id;

            CreateTabs();
        }

        private void CreateTabs()
        {
            CreateTab(typeof(PlacesActivity), "places", Resource.Drawable.HomeTabPlaces);
            CreateTab(typeof(CategoriesActivity), "categories", Resource.Drawable.HomeTabCategories);
            CreateTab(typeof(ProfileActivity), "profile", Resource.Drawable.HomeTabProfile);
            CreateTab(typeof(SettingsActivity), "settings", Resource.Drawable.HomeTabSettings);

            void CreateTab(Type activityType, string tag, int drawableId)
            {
                var intent = new Intent(this, activityType);
                intent.AddFlags(ActivityFlags.NewTask);

                var spec = TabHost.NewTabSpec(tag);

                spec.SetContent(intent);
                spec.SetIndicator((string)null, GetDrawable(drawableId));

                TabHost.AddTab(spec);
            }
        }
    }
}