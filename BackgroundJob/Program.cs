using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace BackgroundJob
{
    public class Program
    {
        private static readonly string EndpointUri = "<Endpoint>";
        private static readonly string PrimaryKey = "<Secret>";
        private static string databaseId = "BackgroundDatabase";
        private static string containerId = "BackgroundJob";
        private static readonly CosmosClient cosmosClient;
        private static Database database;

        private static Container container;

        static Program()
        {
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).Result;
            container = database.CreateContainerIfNotExistsAsync(containerId, "/id").Result;
        }

        public static async Task Main(string[] args)
        {
            Program p = new Program();
            await p.InsertRecord();
        }

        public async Task<ItemResponse<Company>> InsertRecord()
        {
            Company company = new Company
            {
                CompanyName = "Microsoft"
            };
            // Create a new instance of the Cosmos Client
            ItemResponse<Company> itemResponse = await container.CreateItemAsync<Company>(company, new PartitionKey(company.Id));
            double requestCharge = itemResponse.RequestCharge;
            Console.WriteLine(requestCharge);
            return itemResponse;

        }
    }
}
