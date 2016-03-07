// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ImageDownloader.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		UIKit.UITextField imageURL { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Button != null) {
				Button.Dispose ();
				Button = null;
			}

			if (imageURL != null) {
				imageURL.Dispose ();
				imageURL = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
		}
	}
}
