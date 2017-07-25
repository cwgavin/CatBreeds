
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;

//using Xamarin.Forms;

//namespace CatBreeds
//{
//	public class Grid1 : ContentPage
//	{
//		int count = 1;

//		public Grid1()
//		{
//			var layout = new StackLayout
//			{
//				Orientation = StackOrientation.Vertical, Padding = 50
//			};

//			var grid = new Grid
//			{
//				RowSpacing = 50
//			};

//			grid.Children.Add(new Label { Text = "This" }, 0, 0); // Left, First element
//			grid.Children.Add(new Label { Text = "text is" }, 1, 0); // Right, First element
//			grid.Children.Add(new Label { Text = "in a" }, 0, 1); // Left, Second element
//			grid.Children.Add(new Label { Text = "grid!" }, 1, 1); // Right, Second element

//			var gridButton = new Button { Text = "So is this Button!\nClick me." };
//			gridButton.Clicked += delegate
//			{
//				gridButton.Text = string.Format("Thanks! {0} clicks.", count++);
//			};

//			layout.Children.Add(grid);
//			layout.Children.Add(gridButton);
//			Content = layout;

//		}
//	}
//}
