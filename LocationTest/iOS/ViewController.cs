using System;
		
using UIKit;
using CoreLocation;
using MapKit;
using System.Runtime.InteropServices;
using Foundation;
using System.Runtime.CompilerServices;
using AVFoundation;
using System.Runtime.Remoting.Contexts;

namespace LocationTest.iOS
{
	/* カスタムピンに持たせるデータ群 */
	class CustomAnnotation: MKPointAnnotation
	{
		/*
		 * 必ず「public」で宣言しないと呼び出し側でエラーになる
		 */
		public String data;
	}

	/*
	 * MapViewのDelegate群
	 */
	class MapViewDelegate: MKMapViewDelegate
	{

		private ViewController MainView;

		/*
		 * コンストラクタ的な？（自分も完璧に理解できてない）
		 * MapViewDelegateがMKMapViewDelegateしか継承していないので
		 * Alertなどが使えなかったので、コンストラクタを用いてControllerを引き継いでます。
		 * （説明あってるかわからないけど...）
		 */
		public MapViewDelegate (ViewController view)
		{
			MainView = view;
		}

		/*
		 * ピンのポップアップがタップされたら
		 */
		public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
		{

			var CustomPin = view.Annotation as CustomAnnotation;
			UIAlertController button = UIAlertController.Create ("CustomAnnotation's Data", CustomPin.data, UIAlertControllerStyle.Alert);
			button.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Cancel, null));

			if (control == view.RightCalloutAccessoryView) {

				MainView.PresentViewController (
					button,
					true,
					null
				);
			}
		}

		/*
		 * ピンがMapViewに落とされる時
		 */
		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			const string PIN_ID = "myPin";

			/*
			 * ピンが現在地だったらそのまま
			 * どうやら（annotation is MKUserLocation）は使えないみたい
			 * ObjCRuntime.Runtime.GetNSObject ( IntPrt prt )でXcodeのObject？が拾えるみたい
			 */
			if (ObjCRuntime.Runtime.GetNSObject (annotation.Handle) is MKUserLocation) return null;

			/*
			 * PIN_IDで指定されたピンを取得
			 */
			var pinView = mapView.DequeueReusableAnnotation (PIN_ID) as MKPinAnnotationView;

			/*
			 * PIN_IDで指定されたピンがすでにあるか
			 */
			if (pinView == null) pinView = new MKPinAnnotationView (annotation, PIN_ID);

			/*
			 * ピンタップのポップアップにボタンを表示する
			 */
			pinView.CanShowCallout = true;

			/* 右側に */
			pinView.RightCalloutAccessoryView = new UIButton (UIButtonType.InfoLight);

			/* 左側に */
//			pinView.LeftCalloutAccessoryView = new UIButton (UIButtonType.InfoLight);

			return pinView;
		}
	}

	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif

			CLLocationManager manager = new CLLocationManager ();
			manager.RequestWhenInUseAuthorization ();
			mapView.ShowsUserLocation = true;

			/*
			 * mapViewのDelegateにDelegate群を指定
			 * mapView.delegate = self はできないっぽい
			 * なので、MKMapViewDelegateを継承したクラスを作成し指定した
			 */
			mapView.Delegate = new MapViewDelegate (this);

			var Annotation = new CustomAnnotation ();
			Annotation.Coordinate = new CLLocationCoordinate2D (36.0665693, 136.2173733);
			Annotation.Title = "ここにタイトルが入ります";
			Annotation.Subtitle = "ここにサブタイトルが入ります";
			Annotation.data = "各Pinが個別データを持つことができる\n福井県 福井市";
			mapView.AddAnnotation (Annotation);

			var Annotation2 = new CustomAnnotation ();
			Annotation2.Coordinate = new CLLocationCoordinate2D (35.6896385, 139.689912);
			Annotation2.Title = "ここにタイトルが入ります";
			Annotation2.Subtitle = "ここにサブタイトルが入ります";
			Annotation2.data = "各Pinが個別データを持つことができる\n東京都 東京都庁";
			mapView.AddAnnotation (Annotation2);

			// iOS 8 has additional permissions requirements
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				manager.RequestAlwaysAuthorization (); // works in background
				//manager.RequestWhenInUseAuthorization (); // only in foreground
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				manager.AllowsBackgroundLocationUpdates = true;
			}

			/* Locationが更新された際にコールされる */
			manager.LocationsUpdated += (sender, e) => {
				foreach (var loc in e.Locations) {
					Console.WriteLine (loc);
				}
			};

			manager.StartUpdatingLocation ();
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
