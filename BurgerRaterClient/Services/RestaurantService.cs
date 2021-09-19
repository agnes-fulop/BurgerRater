using BurgerRaterClient.Models.Dtos;
using BurgerRaterClient.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BurgerRaterClient.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string _burgerClientScope = string.Empty;
        private readonly string _burgerClientBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public RestaurantService(ITokenAcquisition tokenAcquisition,
                               HttpClient httpClient,
                               IConfiguration configuration,
                               IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _burgerClientScope = configuration["AzureAdB2C:Scope"];
            _burgerClientBaseAddress = configuration["AppSettings:BurgerRaterApiBaseAddress"];
        }

        public async Task<IEnumerable<RestaurantResponseDto>> GetAllRestaurants()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{ _burgerClientBaseAddress}/Restaurants");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                IEnumerable<RestaurantResponseDto> restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantResponseDto>>(content);

                return restaurants;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _burgerClientScope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
