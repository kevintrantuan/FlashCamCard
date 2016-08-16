
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
	[Activity(Label = "CardListActivity")]
	public class CardListActivity : Activity
	{
		string voc;
		private List<string> mItems;
		private ListView mListView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.CardList);

			mListView = FindViewById<ListView>(Resource.Id.cardList);
			mItems = new List<string>();
			mItems.Add("voc");
			mItems.Add("definition");
			mItems.Add("image");
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);
			mListView.Adapter = adapter;

		}
	}
}

