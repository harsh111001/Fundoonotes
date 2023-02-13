using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness user;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBusiness user,ILogger<UserController> logger)
        {
            this.user = user;
            this.logger = logger;
        }
        //https://localhost:44346/api/User/Register
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
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var result=user.GetAllUsers();
            return Ok(result);
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            var loginData = user.Login(model);
            if(loginData != null)
            {
                logger.LogInformation($"user {model.EmailId} logged in");
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
        [Authorize]
        [HttpPatch("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword reset)
        {
            var Email = User.Claims.FirstOrDefault(a => a.Type == "EmailId").Value;
            var forget = user.ResetPassword(reset,Email);
            if (forget != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Password Reset", Data = forget });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Failed" });
            }
        }
    }
}
