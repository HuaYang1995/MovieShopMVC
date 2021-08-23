using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var user = _userService.RegisterUser(model);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterGet([FromBody] UserRegisterRequestModel model)
        {
            var user = _userService.RegisterUser(model);
            return Ok(user);
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user_login = _userService.Login(model);
            return Ok(user_login);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {

            var GetId = _userService.GetById(id);
            return Ok(GetId);
        }
    }
}