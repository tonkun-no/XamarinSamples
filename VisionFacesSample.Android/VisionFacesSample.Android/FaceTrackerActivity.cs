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

using Android.Gms.Vision;
using Android.Gms.Vision.Faces;
using Android.Graphics;

namespace VisionFacesSample.Android
{
    [Activity (Label = "Face Tracker")]
    public class FaceTrackerActivity
        : Activity
    {
        private CameraSourcePreview _cameraSourcePreview;
        private CameraSource _cameraSource;
        private GraphicOverlay _overlay;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.FaceTracker);

            _cameraSourcePreview = FindViewById<CameraSourcePreview>(Resource.Id.cameraSourcePreview);
            _overlay = FindViewById<GraphicOverlay>(Resource.Id.faceOverlay);

            var detector = new FaceDetector.Builder(Application.Context)
                //.SetTrackingEnabled(false)
                .SetLandmarkType(LandmarkDetectionType.All)
                .SetMode(FaceDetectionMode.Accurate)
                .Build();
            detector.SetProcessor(
                new MultiProcessor.Builder(new FaceTrackerFactory(_overlay)).Build());

            _cameraSource = new CameraSource.Builder(this, detector)
                .SetAutoFocusEnabled(true)
                //.SetRequestedPreviewSize(640, 480)
                .SetFacing(CameraFacing.Front)
                .SetRequestedFps(30.0f)
                .Build();
        }


        protected override void OnResume()
        {
            base.OnResume();
            _cameraSourcePreview.Start(_cameraSource, _overlay);
        }
        protected override void OnPause()
        {
            base.OnPause();
            _cameraSourcePreview.Stop();
        }

        protected override void OnDestroy()
        {
            _cameraSourcePreview.Release();
            base.OnDestroy();
        }
    }
}