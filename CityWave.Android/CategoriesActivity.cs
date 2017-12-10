using Android.App;
using Android.OS;
using Android.Widget;

namespace CityWave.Android
{
    [Activity(Label = "CategoriesActivity")]
    public class CategoriesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Categories);

            var CategoriesList = FindViewById<ListView>(Resource.Id.CategoriesListView);
            var adapter = new CategoriesListAdapter(this);

            CategoriesList.Adapter = adapter;

            adapter.ItemsLoading += ()
                => FindViewById<ProgressBar>(Resource.Id.CategoriesProgressBar).Visibility = global::Android.Views.ViewStates.Visible;

            adapter.ItemsLoaded += ()
                => FindViewById<ProgressBar>(Resource.Id.CategoriesProgressBar).Visibility = global::Android.Views.ViewStates.Invisible;

            adapter.LoadItems();
        }
    }
}