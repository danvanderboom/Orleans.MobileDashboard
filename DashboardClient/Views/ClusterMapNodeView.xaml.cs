using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DashboardClient.Views
{
	public partial class ClusterMapNodeView : ContentView
	{
		public ClusterMapNodeView ()
		{
			InitializeComponent ();

			SizeChanged += ClusterMapNodeView_SizeChanged;
		}

		void ClusterMapNodeView_SizeChanged (object sender, EventArgs e)
		{
			
		}
	}
}

