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

namespace FlashCamCard.Droid
{
	public static class App
	{
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}
	[Activity(Label = "FlashCamCard.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Button btnLearn;
		ImageButton imgBtnCamera;
		string voc;
		private List<string> mItems;
		private ListView mListView;

		private ImageView _imageView;

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


			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				imgBtnCamera = FindViewById<ImageButton>(Resource.Id.imgBtnCam);
				imgBtnCamera.Click += TakeAPicture;
				_imageView = FindViewById<ImageView>(Resource.Id.imageCamView);

			}


			mListView = FindViewById<ListView>(Resource.Id.cardList);
			mItems = new List<string>();
			mItems.Add("voc");
			mItems.Add("definition");
			mItems.Add("image");
			ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);
			mListView.Adapter = adapter;

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
			base.OnActivityResult(requestCode, resultCode, data);

			// Make it available in the gallery

			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Uri contentUri = Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
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

		void btnLearn_Click (object sender, System.EventArgs e)
		{
			var intent = new Intent(this, typeof(CardActivity));
			voc = "Tuan";
			intent.PutExtra("card",voc);
			StartActivity(intent);
		}
	}
}


