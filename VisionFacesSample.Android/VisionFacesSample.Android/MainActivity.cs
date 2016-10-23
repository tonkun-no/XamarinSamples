using System;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Gms.Vision;
using Android.Gms.Vision.Faces;
using Android.Graphics;

namespace VisionFacesSample.Android
{
    /// <summary>
    /// メイン画面用
    /// </summary>
    [Activity(Label = "VisionFacesSample.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _buttonFaceTracker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _buttonFaceTracker = FindViewById<Button>(Resource.Id.buttonFaceTracker);

            _buttonFaceTracker.Click += (arg, e) =>
            {
                // 画面遷移
                StartActivity(typeof(FaceTrackerActivity));
            };

        }
    }
}

