using System;
		
using UIKit;
using System.Net;
using System.IO;
using System.Text;
using Foundation;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

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

			Button.TouchUpInside += async (sender, e) => {
				if (imageURL.HasText) {
					try {
						byte[] B_imageData = await Download(imageURL.Text);
						NSData imageData = NSData.FromArray(B_imageData);
						ImageView.Image = UIImage.LoadFromData(imageData);
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

		async Task<byte[]> Download (string Url)
		{
			var httpClient = new HttpClient ();
			return await httpClient.GetByteArrayAsync (Url);
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