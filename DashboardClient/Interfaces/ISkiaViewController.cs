using System;
using SkiaSharp;
using Xamarin.Forms;

namespace DashboardClient
{
	public interface ISkiaViewController : IViewController
	{
		void SendDraw (SKCanvas canvas);
	}
}
