using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Gms.Vision;
using Android.Gms.Vision.Faces;

namespace VisionFacesSample.Android
{
    public class FaceGraphic
        : GraphicOverlay.Graphic
    {
        private const float FACE_POSITION_RADIUS = 10.0f;
        private const float ID_TEXT_SIZE = 40.0f;
        private const float ID_Y_OFFSET = 50.0f;
        private const float ID_X_OFFSET = -50.0f;
        private const float BOX_STROKE_WIDTH = 5.0f;

        private readonly Color[] COLOR_CHOICES = {
            Color.Blue,
            Color.Cyan,
            Color.Green,
            Color.Magenta,
            Color.Red,
            Color.White,
            Color.Yellow
        };

        private static int colorIndex = 0;

        private Paint _facePositionPaint;
        private Paint _idPaint;
        private Paint _boxPaint;

        private Face _face;

        public int Id { get; set; }

        public FaceGraphic(GraphicOverlay overlay) : base(overlay)
        {
            colorIndex = (colorIndex + 1) % COLOR_CHOICES.Length;
            var selectedColor = COLOR_CHOICES[colorIndex];

            _facePositionPaint = new Paint();
            _facePositionPaint.Color = selectedColor;

            _idPaint = new Paint();
            _idPaint.Color = selectedColor;
            _idPaint.TextSize = ID_TEXT_SIZE;

            _boxPaint = new Paint();
            _boxPaint.Color = selectedColor;
            _boxPaint.SetStyle(Paint.Style.Stroke);
            _boxPaint.StrokeWidth = BOX_STROKE_WIDTH;
        }

        public void UpdateFace(Face face)
        {
            _face = face;
            base.PostInvalidate();
        }

        public override void Draw(Canvas canvas)
        {
            Face face = _face;
            if (face == null)
                return;

            float x = base.TranslateX(face.Position.X + face.Width / 2);
            float y = base.TranslateY(face.Position.Y + face.Height / 2);
            canvas.DrawCircle(x, y, FACE_POSITION_RADIUS, _facePositionPaint);
            canvas.DrawText(string.Format("id:{0}", Id), x + ID_X_OFFSET, y + ID_Y_OFFSET, _idPaint);

            float xOffset = base.ScaleX(face.Width / 2.0f);
            float yOffset = base.ScaleY(face.Height / 2.0f);
            float left = x - xOffset;
            float top = y - yOffset;
            float right = x + xOffset;
            float bottom = y + yOffset;

            canvas.DrawRect(left, top, right, bottom, _boxPaint);
        }
    }
}