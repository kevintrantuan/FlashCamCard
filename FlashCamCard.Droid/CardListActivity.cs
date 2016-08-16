
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

			cardList = new List<Card>();
			cardList.Add(new Card()
			{
				voc = "pragmatics",
				defintion = "solving problems in a practical and sensible way rather than by having fixed ideas or theories",
				imagefile = "pragmatics.jpg"
			});
			cardList.Add(new Card()
			{
				voc = "sprint",
				defintion = "a race in which the people taking part run, swim, etc. very fast over a short distance",
				imagefile = "sprint.jpg"
			});
			cardList.Add(new Card()
			{
				voc = "prosperity",
				defintion = "the state of being successful and having a lot of money",
				imagefile = "prosperity.jpg"
			});

			var listAdapter = new CardsAdapter(this);
			listAdapter.cards = cardList;
			ListAdapter = listAdapter;

		}
	}
	public class Card
	{
		public int id;
		public string voc;
		public string defintion;
		public string imagefile;
	}
}

