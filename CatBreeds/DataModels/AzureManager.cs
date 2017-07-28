using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace CatBreeds.DataModels
{
	public class AzureManager
	{
		private static AzureManager instance;
		private MobileServiceClient client;
        private IMobileServiceTable<CatBreedsModel> catBreedsTable;

		private AzureManager()
		{
			client = new MobileServiceClient("http://catbreeds.azurewebsites.net");
            catBreedsTable = client.GetTable<CatBreedsModel>();
		}

		public MobileServiceClient AzureClient
		{
			get { return client; }
		}

		public static AzureManager AzureManagerInstance
		{
			get
			{
				if (instance == null) instance = new AzureManager();
				return instance;
			}
		}

		public async Task<List<CatBreedsModel>> GetCatBreeds()
		{
            return await catBreedsTable.ToListAsync();
		}

        public async Task PostCatBreedsInformation(CatBreedsModel catBreedsModel)
		{
            await catBreedsTable.InsertAsync(catBreedsModel);
		}

		public async Task UpdateCatBreedsInformation(CatBreedsModel catBreedsModel)
		{
            await catBreedsTable.UpdateAsync(catBreedsModel);
		}
		public async Task DeleteCatBreedsInformation(CatBreedsModel catBreedsModel)
		{
            await catBreedsTable.DeleteAsync(catBreedsModel);
		}
	}
}
