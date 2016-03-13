using Android.App;
using Android.Widget;
using Android.OS;
using Java.Nio;
using Android.Gms.Maps;
using System;
using System.Security.Cryptography;
using Android.Gms.Maps.Model;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Java.Util;
using Android.Support.V4.App;

namespace LocationTest.Droid
{

	[Activity (Label = "LocationTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : FragmentActivity, IOnMapReadyCallback
	{
		public Dictionary<int, String> CustomData = new Dictionary<int, String>();
		static GoogleMap mMap = null;
		static MapFragment mapFragment;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (global::LocationTest.Droid.XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it

			mapFragment = FragmentManager.FindFragmentById (Resource.Id.map) as MapFragment;

			mapFragment.GetMapAsync (this);
		}

		public void OnMapReady (GoogleMap googleMap)
		{
			mMap = googleMap;

			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(new LatLng(35.6896385, 139.689912)).Zoom(4);
			CameraPosition cameraPosition = builder.Build();
			CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

			mMap.MoveCamera(cameraUpdate);

			mMap.MarkerClick += (object sender, GoogleMap.MarkerClickEventArgs e) => {
				Toast.MakeText (this, CustomData[e.Marker.GetHashCode()], ToastLength.Short).Show ();
				e.Handled = false;
			};

			MarkerOptions option = new MarkerOptions ();
			option.SetPosition (new LatLng (36.0665693, 136.2173733));
			option.SetTitle ("福井県 福井市");
			String data = "CustomMarker's Data";
			CustomData.Add (mMap.AddMarker (option).GetHashCode(), data);

			MarkerOptions option2 = new MarkerOptions ();
			option2.SetPosition (new LatLng (35.6896385, 139.689912));
			option2.SetTitle ("東京都 東京都庁");
			String data2 = "CustomMarker's Data2";
			CustomData.Add (mMap.AddMarker (option2).GetHashCode(), data2);
		}
	}
}
