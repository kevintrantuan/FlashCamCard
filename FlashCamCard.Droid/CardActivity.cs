
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace FlashCamCard.Droid
{
	[Activity(Label = "Learning Card")]
	public class CardActivity : Activity
	{
		TextView vocTextView;
		ImageView vocImageFileName;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Card);
			vocTextView = FindViewById<TextView>(Resource.Id.textVoc);
			vocImageFileName = FindViewById<ImageView>(Resource.Id.imagePhotoView);

			IList<string> card = Intent.Extras.GetStringArrayList("card");
			vocTextView.Text = card[1];
			if (card[2] != null)
			{
				vocImageFileName.SetImageURI(Uri.Parse(card[2]));
			}
		}
	}
}

