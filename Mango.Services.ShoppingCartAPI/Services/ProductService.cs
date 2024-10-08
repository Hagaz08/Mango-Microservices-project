﻿using Mango.Service.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Services.IServices;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Services
{
    public class ProductService : IProductService

    { 
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory ClientFactory)
        {
            _httpClientFactory = ClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent= await response.Content.ReadAsStringAsync();
            var resp= JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.isSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
