using System;
		
using UIKit;
using System.Net;
using System.IO;
using System.Text;
using Foundation;
using System.Linq.Expressions;

namespace ImageDownloader.iOS
{
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

			// Perform any additional setup after loading the view, typically from a nib.
			Button.TouchUpInside += delegate {
				// "" -> HasText: false
				// "a" -> HasText: true
				if (imageURL.HasText) {
					try {
						ImageView.Image = DownloadImage (NSUrl.FromString (imageURL.Text));
					} catch {
						ShowAlert ("エラー", "URLが正しくありません", new String[]{ "OK" });
					}
				} else {
					ShowAlert ("エラー", "URLが入力されていません", new String[]{ "OK" });
				}
			};
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}

		public UIImage DownloadImage (NSUrl Url)
		{
			var webClient = new WebClient ();
			var ImageData = webClient.DownloadData (Url);
			return UIImage.LoadFromData (NSData.FromArray (ImageData));
		}

		public void ShowAlert (String title, String subtitle, String[] button)
		{
			//Create Alert
			var okAlertController = UIAlertController.Create (title, subtitle, UIAlertControllerStyle.Alert);

			foreach (String buttonName in button) {			
				//Add Action
				okAlertController.AddAction (UIAlertAction.Create (buttonName, UIAlertActionStyle.Default, null));
			}

			// Present Alert
			PresentViewController (okAlertController, true, null);
		}
	}
}