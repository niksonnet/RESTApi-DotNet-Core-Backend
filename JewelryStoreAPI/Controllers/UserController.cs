using AutoMapper;
using JewelryStoreAPI.DTO;
using JewelryStoreAPI.Entity;
using JewelryStoreAPI.Helper;
using JewelryStoreAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JewelryStoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Declarations
            private IUserService _userService;
            private IMapper _mapper;
            private readonly Setting _appSettings;
        #endregion

        #region Ctor
        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<Setting> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region Actions
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserModel userModel)
        {
            var user = _userService.Authenticate(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            var tokenString = UtilityService.GenerateJWT(user.Id.ToString(), _appSettings.Secret); ;
            
            return Ok(new
            {
               // Id = user.Id,
                //FirstName = user.FirstName,
                //LastName = user.LastName,
                Username = user.Username,
                Role = user.Role,
                Discount = new { Percentage = user.Discount.Percentage },
                Token = tokenString
            });
        }
        [HttpGet("GetUser")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserModel>(user);
            return Ok(userDto);
        }
        #endregion
    }
}