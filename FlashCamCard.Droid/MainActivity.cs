using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android;

namespace FlashCamCard.Droid
{
	[Activity(Label = "FlashCamCard.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Button btnLearn;
		string voc;
		private List<string> mItems;
		private ListView mListView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			btnLearn = FindViewById<Button>(FlashCamCard.Droid.Resource.Id.btnLearn);
			btnLearn.Click += btnLearn_Click;

			//ImageButton button = FindViewById<ImageButton>(Resource.Id.imgBtnCam);

			mListView = FindViewById<ListView>(Resource.Id.cardList);
			mItems = new List<string>();
			mItems.Add("voc");
			mItems.Add("definition");
			mItems.Add("image");
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);
			mListView.Adapter = adapter;

		}

		void btnLearn_Click (object sender, System.EventArgs e)
		{
			var intent = new Intent(this, typeof(CardActivity));
			voc = "Tuan";
			intent.PutExtra("card",voc);
			StartActivity(intent);
		}
	}
}


