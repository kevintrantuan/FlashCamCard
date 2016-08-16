using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;

namespace FlashCamCard.Droid
{
	public class CardsAdapter : BaseAdapter
	{
		public List<Card> cards;
		Activity activity;

		public CardsAdapter(Activity activity)
		{
			cards = new List<Card>();
			this.activity = activity;
		}

		public override int Count
		{
			get
			{
				return cards.Count;
			}
		}

		public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.CardCell, parent, false);
			var vocLabel = view.FindViewById<TextView>(Resource.Id.textViewVoc);
			var defintionLabel = view.FindViewById<TextView>(Resource.Id.textViewDefinition);
			var vocPhotoImageView = view.FindViewById<ImageView>(Resource.Id.imageViewVocPhoto);

			var data = cards[position];

			vocLabel.Text = data.voc;
<<<<<<< HEAD
			defintionLabel.Text = data.defintion;//.Substring(0, 20) + "...";
=======
			defintionLabel.Text = data.defintion.Substring(0, 20) + "...";
>>>>>>> parent of 9fbd7c7... Jason transfer between activity
			vocPhotoImageView.SetImageResource(Resource.Drawable.ic_photo_library_white_48dp);

			return view;


		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return cards[position].id;
		}

	}
}

