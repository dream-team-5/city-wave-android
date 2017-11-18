using Android.App;
using Android.OS;

namespace CityWave.Android
{
    [Activity(Label = "CategoriesActivity")]
    public class CategoriesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Categories);
        }
    }
}