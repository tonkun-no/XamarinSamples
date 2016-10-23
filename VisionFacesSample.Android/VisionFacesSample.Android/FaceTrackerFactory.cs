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
using Java.Lang;

namespace VisionFacesSample.Android
{
    public class FaceTrackerFactory
        : Java.Lang.Object, MultiProcessor.IFactory
    {
        public GraphicOverlay Overlay { get; private set; }

        public FaceTrackerFactory(GraphicOverlay overlay)
            :base()
        {
            Overlay = overlay;
        }

        public Tracker Create(Java.Lang.Object item)
        {
            return new FaceTracker(Overlay);
        }
    }

    internal class FaceTracker
        : Tracker
    {
        private GraphicOverlay _overlay;
        private FaceGraphic _faceGraphic;

        public FaceTracker(GraphicOverlay overlay)
        {
            _overlay = overlay;
            _faceGraphic = new FaceGraphic(overlay);
        }

        public override void OnNewItem(int id, Java.Lang.Object item)
        {
            _faceGraphic.Id = id;
        }

        public override void OnUpdate(Detector.Detections detections, Java.Lang.Object item)
        {
            _overlay.Add(_faceGraphic);
            _faceGraphic.UpdateFace((Face)item);
        }

        public override void OnMissing(Detector.Detections detections)
        {
            _overlay.Remove(_faceGraphic);
        }

        public override void OnDone()
        {
            _overlay.Remove(_faceGraphic);
        }

    }
}