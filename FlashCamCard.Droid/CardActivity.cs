
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

namespace FlashCamCard.Droid
{
	[Activity(Label = "CardActivity")]
	public class CardActivity : Activity
	{
		TextView vocTextView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Card);
			vocTextView = FindViewById<TextView>(Resource.Id.textVoc);
			string strVoc = Intent.Extras.GetString("card");
			vocTextView.Text = strVoc;
		}
	}
}

