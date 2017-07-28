using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Tabs.Model;
using Xamarin.Forms;
using Plugin.Geolocator;
using CatBreeds.DataModels;
using System.Diagnostics;

namespace CatBreeds
{
    public partial class CatBreedsPage : ContentPage
    {
        public CatBreedsPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
        	Button button = (Button)sender;
            await Navigation.PushModalAsync(new CatBreedsHistory());
        }

        private async void TakePhoto(object s, EventArgs e)
        {
            // check if the camera is available 
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new StoreCameraMediaOptions
                {
                    Directory = "temp",
                    Name = $"{DateTime.UtcNow}.jpg"
                };
                // Take a photo. 
                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
				// To diplay the image 
                if (file == null){
                    return;
                }
                //await DisplayAlert("File Location", file.Path, "OK");
				photo.Source = ImageSource.FromStream(() =>
				{
                    var stream = file.GetStream();
					return stream;
				});
				result.Text = "Analyzing... Please wait";
				await postLocationAsync();
				await Analyze(file);
            }
            else {
                await DisplayAlert("No Camera", "There is no camera available.", "OK");
            }
        }
        private async void PickPhoto(object s, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var file = await CrossMedia.Current.PickPhotoAsync();
				if (file == null)
				{
					return;
				}
				photo.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					return stream;
				});
                result.Text = "Analyzing... Please wait";
                await postLocationAsync();
				await Analyze(file);
			}
			else
			{
				await DisplayAlert("Error", "Cannot pick a photo.", "OK");
			}
        }

        async Task Analyze(MediaFile file)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-Key", "08af1588c4a6476ebc2d79588bbcdb24");
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/8f1d90ad-1cf2-49f0-b44e-29366efdcb0e/image";

            //byte[] byteData = GetImageAsByteArray(file);
			var stream = file.GetStream();
			var binaryReader = new BinaryReader(stream);
			var length = (int)stream.Length;
			byte[] byteData = binaryReader.ReadBytes(length);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(url, content);
               
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    EvaluationModel responseModel = JsonConvert.DeserializeObject<EvaluationModel>(responseString);

                    string resultText = "";
                    foreach (var breed in responseModel.Predictions){
                        double prob = breed.Probability >= 0.0001 ? breed.Probability : 0;
                        resultText += breed.Tag + ": " + Math.Round(prob * 100, 2) + "%\n";
                    }
                    result.Text = resultText;
                }
                file.Dispose();
            }
        }
		async Task postLocationAsync()
		{
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(1));

                CatBreedsModel model = new CatBreedsModel()
                {
                    Longitude = (float)position.Longitude,
                    Latitude = (float)position.Latitude
                };

                await AzureManager.AzureManagerInstance.PostCatBreedsInformation(model);
                //await DisplayAlert("Pushed", "Data Pushed", "OK");
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
			}
		}

    }
}
