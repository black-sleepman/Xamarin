using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Graphics;
using System.Threading.Tasks;
using Java.Lang;
using System.Net;
using Android.Net;
using System.Linq.Expressions;
using System;
using System.Reflection;
using System.Net.Http;

namespace ImageDownloader.Droid
{
	[Activity (Label = "ImageDownloader", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (global::ImageDownloader.Droid.XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			Button getButton = FindViewById<Button> (Resource.Id.button);
			EditText imageURL = FindViewById<EditText> (Resource.Id.imageURL);
			ImageView imageView = FindViewById<ImageView> (Resource.Id.imageView);

			getButton.Click += async (sender, e) =>  {

				if (imageURL.Text.Equals("")) {
					
					Toast.MakeText (this, "URLが入力されていません", ToastLength.Short).Show();
				}
				else {
					
					var URL = imageURL.Text;

					try {
						byte[] ImageData = await Download(URL);
						imageView.SetImageBitmap(BitmapFactory.DecodeByteArray(ImageData, 0, ImageData.Length));
					}
					catch {
						Toast.MakeText(this, "URLが正しくありません", ToastLength.Short).Show();
						imageView.SetImageBitmap(null);
					}
				}
			};
		}

		async Task<byte[]> Download(string Url){
			var httpClient = new HttpClient();
			return await httpClient.GetByteArrayAsync(Url);
		}
	}
}
