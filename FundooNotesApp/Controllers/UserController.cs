using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness user;
        public UserController(IUserBusiness user)
        {
            this.user = user;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            var registerData = user.Register(model);
            if(registerData != null)
            {
                return Ok(new ResponseModel<UserEntity>{Status=true,Message="Registered successfully",Data=registerData});
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> {Status=false,Message="registeration failed",});
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            var loginData = user.Login(model);
            if(loginData != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "login successful",Data=loginData});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "login failed", });
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            var forget=user.ForgetPassword(Email);
            if(forget!=null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Mail Sent", Data = forget });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Mail not sent" });
            }
        }
    }
}
