using System.Net.Http;
using System.Threading.Tasks;
using MicroRabbit.MVC.Models.DTO;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

namespace MicroRabbit.MVC.Services
{
    /**
     * Transfer Service class
     * 
     * Local proxy banking service.
     * 
     * @author D. P. Edwards
     * @license MIT
     * @version 1.0
     */
    public class TransferService : ITransferService
    {
        private readonly HttpClient _apiClient;
        IConfiguration _configuration;

        public TransferService(HttpClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public async Task Transfer(TransferDto transferDto)
        {
            var url = _configuration.GetValue(typeof(string), "BANKING_ENDPOINT");

            // var uri = "https://localhost:5001/api/Banking";
            var uri = url + "/api/Banking";
            var transferContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(transferDto),
                                            System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, transferContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
