using System;
using Xamarin.Forms;

public static class GridExtensions
{
	public static void PlaceInGrid (this BindableObject view, int row, int column, int rowSpan = 1, int columnSpan = 1)
	{
		Grid.SetRow (view, row);
		Grid.SetColumn (view, column);
		Grid.SetRowSpan (view, rowSpan);
		Grid.SetColumnSpan (view, columnSpan);
	}

	public static void SetGridRow (this BindableObject view, int row, int rowSpan = 1)
	{
		Grid.SetRow (view, row);
		Grid.SetRowSpan (view, rowSpan);
	}

	public static void SetGridColumn (this BindableObject view, int column, int columnSpan = 1)
	{
		Grid.SetColumn (view, column);
		Grid.SetColumnSpan (view, columnSpan);
	}
}
