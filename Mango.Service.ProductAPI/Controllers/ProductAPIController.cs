using AutoMapper;
using Mango.Service.ProductAPI.Models;
using Mango.Service.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Service.ProductAPI.Controllers
{
	[Route("api/product")]
	[ApiController]
	//[Authorize]
	public class ProductAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public ProductAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDto();
		}
		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Product> objList = _db.Products.ToList();
				_response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);
				// return _response;
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}


		[HttpGet]
		[Route("{id:int}")]
		public ResponseDto Get(int id)
		{
			try
			{
				Product obj = _db.Products.First(x => x.ProductId == id);
				_response.Result = _mapper.Map<ProductDto>(obj);
				//return _response;
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpGet]
		[Route("GetByCategory/{categoryName}")]
		public ResponseDto Get(string categoryName)
		{
			try
			{
				var obj = _db.Products.Where(x => x.CategoryName.ToLower() == categoryName.ToLower()).AsEnumerable(); ;
				_response.Result = _mapper.Map<IEnumerable<ProductDto>>(obj);
				//return _response;
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Post([FromBody] ProductDto productDto)
		{
			try
			{
				Product newProduct = _mapper.Map<Product>(productDto);
				_db.Products.Add(newProduct);
				_db.SaveChanges();

				_response.Result = _mapper.Map<ProductDto>(newProduct);
				_response.Message = "Product created Succesfully";
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPut]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Put([FromBody] ProductDto productDto)
		{
			try
			{
				Product newProduct = _mapper.Map<Product>(productDto);
				_db.Products.Update(newProduct);
				_db.SaveChanges();

				_response.Result = _mapper.Map<ProductDto>(newProduct);
				_response.Message = "Product Updated Succesfully";
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpDelete]
		[Authorize(Roles = "ADMIN")]
		[Route("{id:int}")]
		public ResponseDto Delete(int id)
		{
			try
			{
				var productToDelete = _db.Products.First(x => x.ProductId == id);
				_db.Products.Remove(productToDelete);
				_db.SaveChanges();
				_response.Message = "Product Deleted Succesfully";
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}

