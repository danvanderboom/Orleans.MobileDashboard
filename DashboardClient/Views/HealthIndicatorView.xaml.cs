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

					DrawArc (canvas, paint, 0, 90);

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
	}
}
