using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;

		public ProductService(IBaseService baseService)
        {
			_baseService = baseService;
		}
        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.POST,
				Data = productDto,
				Url = SD.ProductAPIbase + "/api/product"
			});
		}

		public async Task<ResponseDto?> DeleteProductAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.ProductAPIbase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIbase + "/api/product"
			});
		}

		public async Task<ResponseDto?> GetProductbyCategoryAsync(string categoryName)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIbase + "/api/product/GetByCategory/" + categoryName
			});
		}

		public async Task<ResponseDto?> GetProductByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIbase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.PUT,
				Data = productDto,
				Url = SD.ProductAPIbase + "/api/product"
			});
		}
	}
}
