
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
using System.Collections.Generic;

namespace FlashCamCard.Droid
{
	[Activity(Label = "Learning Card List")]
	public class CardListActivity : ListActivity
	{

		List<Card> cardList;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			//SetContentView(Resource.Layout.CardList);

			cardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(Intent.GetStringExtra("cardlist"));

			var listAdapter = new CardsAdapter(this);
			listAdapter.cards = cardList;
			ListAdapter = listAdapter;

		}
	}

}

