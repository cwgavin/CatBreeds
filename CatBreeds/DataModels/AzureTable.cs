using System;

using Xamarin.Forms;

namespace CatBreeds.DataModels
{
    public class AzureTable : ContentPage
    {
        public AzureTable()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

