using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinTest.Droid
{
	[Activity (Label = "XamarinTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (global::XamarinTest.Droid.XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView Counter = FindViewById<TextView> (Resource.Id.CounterView);
			button.Click += delegate {
				Counter.Text = string.Format ("{0} clicks!", count++);
			};
		}
	}
}
