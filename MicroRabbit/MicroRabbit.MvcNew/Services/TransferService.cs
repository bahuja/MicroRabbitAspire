using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MicroRabbit.MVC.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
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
        private IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisition;
        public TransferService(ITokenAcquisition tokenAcquisition, HttpClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
            _tokenAcquisition= tokenAcquisition;
        }

        public async Task Transfer(TransferDto transferDto)
        {
            await AquireToken();

            var url = _configuration.GetValue(typeof(string), "BANKING_ENDPOINT");

            // var uri = "https://localhost:5001/api/Banking";
            var uri = url + "/api/Banking";
            var transferContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(transferDto),
                                            System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, transferContent);
            response.EnsureSuccessStatusCode();
        }

        private async Task AquireToken()
        {
            var scope = "api://265c95e9-fc5b-4e2a-9c3c-ca979515b317/User.Read";
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope });
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
