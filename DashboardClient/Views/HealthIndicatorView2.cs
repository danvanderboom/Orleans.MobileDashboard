//using System;
//using System.Collections.Generic;
//using Xamarin.Forms;
//using NControl.Abstractions;
//using NGraphics;

//namespace DashboardClient.Views
//{
//	public class HealthIndicatorView : NControlView
//	{
//		public static BindableProperty ValueProperty =
//			BindableProperty.Create("Value", typeof(double), typeof(HealthIndicatorView), 0.5, 
//			                        BindingMode.OneWay, null, ValueChanged);

//		public double Value 
//		{
//			get { return (double)GetValue (ValueProperty); }
//			set { SetValue (ValueProperty, value); }
//		}

//		static void ValueChanged (BindableObject bindable, object oldValue, object newValue)
//		{
//			(bindable as HealthIndicatorView).Invalidate ();
//		}



//		public static readonly BindableProperty StartAngleProperty =
//			BindableProperty.Create("StartAngle", typeof (double), typeof (HealthIndicatorView), 0D,
//									BindingMode.OneWay, null, StartAngleChanged);

//		public double StartAngle 
//		{
//			get { return (double)GetValue (StartAngleProperty); }
//			set { SetValue (StartAngleProperty, value); }
//		}

//		static void StartAngleChanged (BindableObject bindable, object oldValue, object newValue)
//		{
//			(bindable as HealthIndicatorView).Invalidate ();
//		}



//		public static readonly BindableProperty EndAngleProperty =
//			BindableProperty.Create("EndAngle", typeof (double), typeof (HealthIndicatorView), 180D,
//									BindingMode.OneWay, null, EndAngleChanged);

//		public double EndAngle 
//		{
//			get { return (double)GetValue (EndAngleProperty); }
//			set { SetValue (EndAngleProperty, value); }
//		}

//		static void EndAngleChanged (BindableObject bindable, object oldValue, object newValue)
//		{
//			(bindable as HealthIndicatorView).Invalidate ();
//		}


//		public override void Draw (NGraphics.ICanvas canvas, NGraphics.Rect rect)
//		{
//			var radius = Bounds.Width < Bounds.Height ? Bounds.Width / 2 : Bounds.Height / 2;

//			double penWidth = 2;
//			var pen = Pens.Blue.WithWidth(penWidth);

//			var startPoint = new NGraphics.Point (Bounds.Width / 2 + radius, Bounds.Height / 2);
//			var endPoint = new NGraphics.Point (Bounds.Width / 2, Bounds.Height / 2 - radius);



//			canvas.DrawPath (new PathOp []
//			{
//				new MoveTo(startPoint),
//				new ArcTo(
//					radius: new NGraphics.Size(radius, radius),
//					largeArc: true,
//					sweepClockwise: true,
//					point: endPoint),
//			},
//			   pen);


//			//var arc = DefineArc (StartAngle, EndAngle, 2, true);
//			//canvas.DrawPath (arc, pen);
//		}

//		PathOp[] DefineArc (double startAngle, double endAngle, double width, bool smallAngle)
//		{
//			var path = new List<PathOp> ();

//			var Center = new NGraphics.Point (Bounds.Width / 2, Bounds.Height / 2);
//			var radius = Bounds.Width < Bounds.Height ? Bounds.Width / 2 : Bounds.Height / 2;

//			var a0 = startAngle < 0 ? startAngle + 2 * Math.PI : startAngle;
//			var a1 = endAngle < 0 ? endAngle + 2 * Math.PI : endAngle;

//			if (a1 < a0)
//				a1 += Math.PI * 2;

//			var sweepClockwise = false;
//			bool large;

//			if (smallAngle)
//			{
//				large = false;
//				double t = a1;
//				if ((a1 - a0) > Math.PI)
//					sweepClockwise = false;
//				else
//					sweepClockwise = true;
//			} 
//			else 
//			{
//				large = (Math.Abs (a1 - a0) < Math.PI);
//			}

//			NGraphics.Point p0 = new NGraphics.Point (
//				x: Center.X + Math.Cos(a0) * radius,
//				y: Center.Y - Math.Sin(a0) * radius
//			);

//			NGraphics.Point p1 = new NGraphics.Point (
//				x: Center.X + Math.Cos (a1) * radius,
//				y: Center.Y - Math.Sin (a1) * radius
//			);


//			//segments.Add (new ArcSegment (p1, new Size (Radius, Radius), 0.0, large, d, true));

//			path.Add (new MoveTo (p0));
//			path.Add (new ArcTo (
//				radius: new NGraphics.Size (radius, radius),
//				largeArc: large,
//				sweepClockwise: sweepClockwise,
//				point: p1));


//			return path.ToArray ();
//		}
//	}
//}

