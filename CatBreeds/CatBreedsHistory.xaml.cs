using System;
using System.Collections.Generic;
using CatBreeds.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace CatBreeds
{
    public partial class CatBreedsHistory : ContentPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;

        public CatBreedsHistory()
        {
            InitializeComponent();
            Handle_ClickedAsync(null, null);
        }

		// set the source of the list view CatBreedsModel to 
		// the list of CatBreedsModel information we got from our backend.
		async void Handle_ClickedAsync(object s, EventArgs e)
		{
			List<CatBreedsModel> catBreedsInformation = await AzureManager.AzureManagerInstance.GetCatBreeds();
			CatBreedsList.ItemsSource = catBreedsInformation;
		}
		async void OnButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;
			await Navigation.PushModalAsync(new CatBreedsPage());
		}
    }
}
