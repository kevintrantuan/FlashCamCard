using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

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

			if (data.defintion.Length > 20)
			{
				defintionLabel.Text = data.defintion.Substring(0, 20) + "...";
			}
			if (data.imagefile.Length > 0)
			{
				vocPhotoImageView.SetImageURI(Uri.Parse(data.imagefile));
			}
			else
			{
				vocPhotoImageView.SetImageResource(Resource.Drawable.ic_photo_library_white_48dp);
			}
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

