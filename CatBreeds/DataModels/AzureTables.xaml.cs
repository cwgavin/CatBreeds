using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CatBreeds.DataModels
{
    public partial class AzureTables : ContentPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;

        public AzureTables()
        {
            InitializeComponent();
        }
    }
}
