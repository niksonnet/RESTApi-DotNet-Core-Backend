using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Services.Services;
using JewelryStoreAPI.DTO;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace JewelryStoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Declarations
        private IUserService _iuserService;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor
        public UserController(
            IUserService iuserService,
            IMapper mapper,
            IConfiguration configuration)
        {
            this._iuserService = iuserService;
            this._mapper = mapper;
            this._configuration = configuration;
        }


        #endregion

        #region Actions
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserModel userModel)
        {
            var user = _iuserService.Authenticate(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var Secret = _configuration.GetValue<string>("AppSettings:Secret");
            var tokenString = UtilityService.GenerateJWT(user.Id.ToString(), Secret);

            return Ok(new
            {
                Username = user.Username,
                Role = user.Role,
                Discount = new { Percentage = user.Discount.Percentage },
                Token = tokenString
            });
        }

        [HttpGet("GetUser")]
        public IActionResult GetById(int id)
        {
            var user = _iuserService.GetById(id);
            
            if (user == null)
                return BadRequest(new { message = "Invalid Request" });

            var userDto = _mapper.Map<UserModel>(user);
            return Ok(userDto);
        }

        [HttpPost("Estimation")]
        public IActionResult CalculateAmount([FromBody]EstimationModel estimation)
        {
            var user = _iuserService.GetByName(estimation.Username);

            if (user == null)
                return BadRequest(new { message = "Invalid Request" });

            decimal discount = user.Discount != null ? user.Discount.Percentage : decimal.Zero;

            var Amount = _iuserService.CalculateFinalAmount(estimation.Rate, estimation.Weight, discount);

            return Ok(new
            {
                Total = Amount,
                Discount = new { Percentage = 0 },
            }); ;
        }
        #endregion
    }
}