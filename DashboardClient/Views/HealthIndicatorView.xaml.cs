using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Prism.Events;
using SkiaSharp;

namespace DashboardClient.Views
{
	public partial class HealthIndicatorView : ContentView
	{
		IUnityContainer Container;
		IEventAggregator Events;

		public HealthIndicatorView()// (IUnityContainer container, IEventAggregator events)
		{
			InitializeComponent ();

			//Container = container;
			//Events = events;

			SizeChanged += OnSizeChanged;
		}

		void OnSizeChanged (object sender, EventArgs e)
		{
			var image = CreateBackgroundImage ();

			var stream = image.Encode ().AsStream ();

			BackgroundImage.Source = ImageSource.FromStream (() => stream);
		}

		public SKImage CreateBackgroundImage ()
		{
			SKPaint paint = null;
			SKPath path = null;

			try 
			{
				var height = (int)Bounds.Height;
				var width = (int)Bounds.Width;

				using (var surface = SKSurface.Create (width, height, SKColorType.N_32, SKAlphaType.Premul)) 
				{
					var canvas = surface.Canvas;

					//canvas.Clear (SKColors.Beige);

					paint = new SKPaint
					{
						Color = SKColors.DarkBlue,
						IsStroke = true,
						StrokeWidth = 1,
						StrokeCap = SKStrokeCap.Round,
						IsAntialias = true
					};

					//path = new SKPath ();
					//path.MoveTo (0f, 0f);
					//path.LineTo (width, height);
					//path.Close ();

					//canvas.DrawPath (path, paint);


					//DrawArc (canvas, paint, 0, 90);

					var start = -90;
					var end = 0;

					//DrawCircle (canvas, paint, width / 2, height / 2, width / 2 - 4);
					DrawArcFromTo (canvas, paint, width / 2, height / 2, width / 2 - 4, start, end);
					DrawArcFromTo (canvas, paint, width / 2, height / 2, (int)((width / 2 - 4) * 0.8), start, end);

					return surface.Snapshot ();
				}
			} 
			finally 
			{
				if (paint != null)
					paint.Dispose ();

				if (path != null)
					path.Dispose ();
			}
		}

		void DrawArc (SKCanvas canvas, SKPaint paint, double startAngleInDegrees, double endAngleInDegrees)
		{
			// find center and radius
			var centerx = (float)Bounds.Width / 2;
			var centery = (float)Bounds.Height / 2;

			var radius = Bounds.Width < Bounds.Height ? (float)Bounds.Width / 2 : (float)Bounds.Height / 2;

			var w = 0.558f;

			// convert degrees to radians
			var startAngleRadians = startAngleInDegrees * Math.PI / 180;
			var endAngleRadians = endAngleInDegrees * Math.PI / 180;

			// find x,y coordinates for start and end points
			var startx = centerx + radius * (float)Math.Cos (startAngleRadians);
			var starty = centery + radius * (float)Math.Sin (startAngleRadians);
			var startPoint = new SKPoint (startx, starty);

			var endx = centerx + radius * (float)Math.Cos (endAngleRadians);
			var endy = centery + radius * (float)Math.Sin (endAngleRadians);
			var endPoint = new SKPoint (endx, endy);

			// find linear distance & midpoint between start and end points
			var linearDistance = Math.Sqrt(Math.Pow(endy - starty, 2) / Math.Pow(endx - startx, 2));
			var midx = (startx + endx) / 2;
			var midy = (starty + endy) / 2;
			var midPoint = new SKPoint (midx, midy);

			// rotate end point 45 degrees counterclockwise around midpoint to find Conic function anchor
			var anchorPoint = RotatePoint (endPoint, midPoint, 45);

			// build path
			var path = new SKPath ();
			//path.MoveTo (startx, starty);
			//path.ConicTo (anchorPoint.X, anchorPoint.Y, endx, endy, w);




			path.MoveTo (centerx, centery - radius);

			path.ConicTo (centerx + radius, centery - radius, centerx + radius, centery, w);
			path.ConicTo (centerx + radius, centery + radius, centerx, centery + radius, w);
			path.ConicTo (centerx - radius, centery + radius, centerx - radius, centery, w);
			//path.ConicTo (centerx - radius, centery - radius, centerx, centery - radius, w);

			//canvas.DrawPoints (SKPointMode.Points, new SKPoint [] { }, paint);

			canvas.DrawPath (path, paint);
		}

		static SKPoint RotatePoint (SKPoint pointToRotate, SKPoint centerPoint, double angleInDegrees)
		{
			double angleInRadians = angleInDegrees * (Math.PI / 180);
			double cosTheta = Math.Cos (angleInRadians);
			double sinTheta = Math.Sin (angleInRadians);

			return new SKPoint 
			{
				X =
					(int)
					(cosTheta * (pointToRotate.X - centerPoint.X) -
					sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
				Y =
					(int)
					(sinTheta * (pointToRotate.X - centerPoint.X) +
					cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
			};
		}

		void DrawCircle (SKCanvas canvas, SKPaint paint, int xc, int yc, int radius)
		{
			if (radius <= 0)
				return;
			
			int x = radius;
			int y = 0;
			int cd2 = 0;

			var points = new List<SKPoint> ();
			points.Add (new SKPoint (xc - radius, yc));
			points.Add (new SKPoint (xc + radius, yc));
			points.Add (new SKPoint (xc, yc - radius));
			points.Add (new SKPoint (xc, yc + radius));

			while (x > y)
			{
				cd2 -= (--x) - (++y);

				if (cd2 < 0) 
					cd2 += x++;

				// 8 octants - listed clockwise

				// right hemisphere, starting at the top
				points.Add (new SKPoint (xc + y, yc - x));
				points.Add (new SKPoint (xc + x, yc - y));
				points.Add (new SKPoint (xc + x, yc + y));
				points.Add (new SKPoint (xc + y, yc + x));

				// left hemisphere, continuing around from the bottom
				points.Add (new SKPoint (xc - y, yc + x));
				points.Add (new SKPoint (xc - x, yc + y));
				points.Add (new SKPoint (xc - x, yc - y));
				points.Add (new SKPoint (xc - y, yc - x));
			}

			canvas.DrawPoints (SKPointMode.Points, points.ToArray (), paint);
		}

		void DrawArcFromTo (SKCanvas canvas, SKPaint paint, int xc, int yc, int radius, 
		                    float startAngleInDegrees, float endAngleInDegrees)
		{
			if (radius <= 0)
				return;

			int x = radius;
			int y = 0;
			int cd2 = 0;



			// convert degrees to radians
			var startAngleRadians = startAngleInDegrees * Math.PI / 180;
			var endAngleRadians = endAngleInDegrees * Math.PI / 180;

			// find x,y coordinates for start and end points
			var startx = xc + radius * (float)Math.Cos (startAngleRadians);
			var starty = yc + radius * (float)Math.Sin (startAngleRadians);
			var startPoint = new SKPoint (startx, starty);

			var endx = xc + radius * (float)Math.Cos (endAngleRadians);
			var endy = yc + radius * (float)Math.Sin (endAngleRadians);
			var endPoint = new SKPoint (endx, endy);

			// build the path
			var points = new List<SKPoint> ();
			//points.Add (new SKPoint (xc - radius, yc));
			//points.Add (new SKPoint (xc + radius, yc));
			//points.Add (new SKPoint (xc, yc - radius));
			//points.Add (new SKPoint (xc, yc + radius));

			while (x > y) 
			{
				x--;
				y++;

				cd2 -= x - y;
				if (cd2 < 0)
					cd2 += x++;

				// 8 octants - listed clockwise

				// right hemisphere, starting at the top
				var p0 = new SKPoint (xc + y, yc - x);
				var arp0 = Math.Atan2 (p0.Y - yc, p0.X - xc);

				var p1 = new SKPoint (xc + x, yc - y);
				var arp1 = Math.Atan2 (p1.Y - yc, p1.X - xc);

				var p2 = new SKPoint (xc + x, yc + y);
				var arp2 = Math.Atan2 (p2.Y - yc, p2.X - xc);

				var p3 = new SKPoint (xc + y, yc + x);
				var arp3 = Math.Atan2 (p3.Y - yc, p3.X - xc);

				// convert radians to degrees
				var ap0 = arp0 * 180 / Math.PI;
				var ap1 = arp1 * 180 / Math.PI;
				var ap2 = arp2 * 180 / Math.PI;
				var ap3 = arp3 * 180 / Math.PI;

				if (IsAngleBetween((int)ap0, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p0);

				if (IsAngleBetween ((int)ap1, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p1);

				if (IsAngleBetween ((int)ap2, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p2);

				if (IsAngleBetween ((int)ap3, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p3);

				// left hemisphere, continuing around from the bottom
				var p4 = new SKPoint (xc - y, yc + x);
				var arp4 = Math.Atan2 (p4.Y - yc, p4.X - xc);

				var p5 = new SKPoint (xc - x, yc + y);
				var arp5 = Math.Atan2 (p5.Y - yc, p5.X - xc);

				var p6 = new SKPoint (xc - x, yc - y);
				var arp6 = Math.Atan2 (p6.Y - yc, p6.X - xc);

				var p7 = new SKPoint (xc - y, yc - x);
				var arp7 = Math.Atan2 (p7.Y - yc, p7.X - xc);

				// convert radians to degrees
				var ap4 = arp4 * 180 / Math.PI;
				var ap5 = arp5 * 180 / Math.PI;
				var ap6 = arp6 * 180 / Math.PI;
				var ap7 = arp7 * 180 / Math.PI;

				if (IsAngleBetween ((int)ap4, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p4);

				if (IsAngleBetween ((int)ap5, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p5);

				if (IsAngleBetween ((int)ap6, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p6);

				if (IsAngleBetween ((int)ap7, (int)startAngleInDegrees, (int)endAngleInDegrees))
					points.Add (p7);
			}

			canvas.DrawPoints (SKPointMode.Points, points.ToArray (), paint);
		}

		bool IsAngleBetween (int target, int angle1, int angle2)
		{
			// make the angle from angle1 to angle2 to be <= 180 degrees
			int rAngle = ((angle2 - angle1) % 360 + 360) % 360;
			if (rAngle >= 180) 
			{
				var tmp = angle1;
				angle1 = angle2;
				angle2 = tmp;
			}

			// check if it passes through zero
			if (angle1 <= angle2)
				return target >= angle1 && target <= angle2;
			else
				return target >= angle1 || target <= angle2;
		}
	}
}
