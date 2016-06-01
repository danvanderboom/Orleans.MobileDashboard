using System;
using Prism.Mvvm;

namespace DashboardClient
{
	public class SiloViewModel : BindableBase
	{
		ViewOrientation _Orientation;
		public ViewOrientation Orientation 
		{
			get { return _Orientation; }
			set { SetProperty (ref _Orientation, value); }
		}
	}
}
