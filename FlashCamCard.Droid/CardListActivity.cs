using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace FlashCamCard.Droid
{
	[Activity(Label = "Learning Card List")]
	public class CardListActivity : Activity
	{

		List<Card> cardList;
		private ListView mListView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.CardList);
			mListView = FindViewById<ListView>(Resource.Id.cardListView);

			cardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(Intent.GetStringExtra("cardlist"));

			var listAdapter = new CardsAdapter(this);
			listAdapter.cards = cardList;
			mListView.Adapter = listAdapter;
			mListView.ItemClick += mListView_ItemClick;
		}

		void mListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			/*
			Console.WriteLine(cardList[e.Position].voc);
			Console.WriteLine(cardList[e.Position].defintion);
			Console.WriteLine(cardList[e.Position].imagefile);
			*/

			List<string> card = new List<string>();
			card.Add(cardList[e.Position].voc);
			card.Add(cardList[e.Position].defintion);
			card.Add(cardList[e.Position].imagefile);

			var intent = new Intent(this, typeof(CardActivity));

			intent.PutStringArrayListExtra("card", card);
			StartActivity(intent);
		}
	}

}

