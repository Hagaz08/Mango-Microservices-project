using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
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
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);
                //return _response;
            }
            catch (Exception ex)
            {
               _response.isSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower()==code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(obj);
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
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon newCoupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(newCoupon);
                _db.SaveChanges();
                
                _response.Result=_mapper.Map<CouponDto>(newCoupon);
				_response.Message = "Coupon created Succesfully";
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
		public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon newCoupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(newCoupon);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(newCoupon);
                _response.Message = "Coupon Updated Succesfully";
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
                var couponToDelete= _db.Coupons.First(x=>x.CouponId==id);
                _db.Coupons.Remove(couponToDelete);
                _db.SaveChanges();
                _response.Message = "Coupon Deleted Succesfully";
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
