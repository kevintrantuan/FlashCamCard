using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Newtonsoft.Json;

namespace FlashCamCard.Droid
{
	public static class App
	{
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}
	[Activity(Label = "Flash Cam Card App", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Button buttonCardList, buttonAddVoc;
		ImageButton imgBtnCamera, imgBtnGallery;
		EditText editVoc, editDefintion;

		public static readonly int PickImageId = 1000;

		private ImageView _imageView;
		string strPhotoName;

		static List<Card> cardList;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			buttonCardList = FindViewById<Button>(Resource.Id.btnCardList);
			buttonCardList.Click += buttonCardList_Click;

			imgBtnGallery = FindViewById<ImageButton>(Resource.Id.imgBtnGallery);
			imgBtnGallery.Click += imgBtnGallery_Click;

			buttonAddVoc = FindViewById<Button>(Resource.Id.btnAddVoc);
			buttonAddVoc.Click += buttonAddVoc_Click;

			editVoc = FindViewById<EditText>(Resource.Id.editVoc);
			editDefintion = FindViewById<EditText>(Resource.Id.editDefinition);


			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				imgBtnCamera = FindViewById<ImageButton>(Resource.Id.imgBtnCam);
				imgBtnCamera.Click += TakeAPicture;
				_imageView = FindViewById<ImageView>(Resource.Id.imagePhotoView);

			}

			buildSampleVoc();

		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Environment.GetExternalStoragePublicDirectory(
					Environment.DirectoryPictures), "FlashCamCard");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
			intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			Uri uri;
			base.OnActivityResult(requestCode, resultCode, data);

			if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
			{
				uri = data.Data;
				_imageView.SetImageURI(uri);
			}
			else
			{

				// Make it available in the gallery

				Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
				uri = Uri.FromFile(App._file);
				mediaScanIntent.SetData(uri);
				SendBroadcast(mediaScanIntent);

				// Display in ImageView. We will resize the bitmap to fit the display.
				// Loading the full sized image will consume to much memory
				// and cause the application to crash.

				int height = Resources.DisplayMetrics.HeightPixels;
				int width = _imageView.Height;
				App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
				if (App.bitmap != null)
				{
					
					_imageView.SetImageBitmap(App.bitmap);
					App.bitmap = null;
				}

				// Dispose of the Java side bitmap.
				GC.Collect();
			}
			strPhotoName = uri.ToString();

		}

		void buttonCardList_Click(object sender, System.EventArgs e)
		{
			var intent = new Intent(this, typeof(CardListActivity));

			intent.PutExtra("cardlist", Newtonsoft.Json.JsonConvert.SerializeObject(cardList));
			StartActivity(intent);
		}
		void imgBtnGallery_Click(object sender, System.EventArgs e)
		{
			Intent = new Intent();
			Intent.SetType("image/*");
			Intent.SetAction(Intent.ActionGetContent);
			StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
		}
		void buttonAddVoc_Click(object sender, System.EventArgs e) {
			if (editVoc.Text.Length > 0)
			{
				cardList.Add(new Card()
				{
					voc = editVoc.Text,
					defintion = editDefintion.Text,
					imagefile = strPhotoName
				});

				editVoc.Text = string.Empty;
				editDefintion.Text = string.Empty;
				_imageView.SetImageResource(Resource.Drawable.ic_camera_enhance_white_48dp);

				//editVoc.Text = strPhotoName;
			}
		}

		void buildSampleVoc()
		{
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
			cardList.Add(new Card()
			{
				voc = "MVC",
				defintion = "Model-View-Controller architecture",
				imagefile = "prosperity.jpg"

			});
			cardList.Add(new Card()
			{
				voc = "procurement",
				defintion = "the state of being successful and having a lot of money",
				imagefile = "prosperity.jpg"

			});
			cardList.Add(new Card()
			{
				voc = "breadwinner",
				defintion = "a person who supports their family with the money they earn",
				imagefile = "prosperity.jpg"

			});
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


