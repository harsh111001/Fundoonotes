using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
     public class UserRepository:IUserRepository
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;
        public UserRepository(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public UserEntity Register(RegisterModel model)
        {
            UserEntity entity= new UserEntity();
            entity.FirstName= model.FirstName;
            entity.LastName= model.LastName;
            entity.EmailId= model.EmailId;
            //if(context.UserTable.Where(u=>u.EmailId==model.EmailId)!=null)
            //{
            //    return null;
            //}
            entity.Password= EncryptPassword(model.Password);
            context.UserTable.Add(entity);
            int res= context.SaveChanges();
            if(res>0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }
        public string EncryptPassword(string password)
        {
            var EncryptPass=System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(EncryptPass);
        }
        public string Login(LoginModel login)
        {
            string encodedPassword=EncryptPassword(login.Password);
            var ifexists=context.UserTable.Where(u=> u.EmailId==login.EmailId && u.Password==encodedPassword).FirstOrDefault();
            if(ifexists==null)
            {
                return null;
            }
            else
            {
                var token = generatejwttoken(ifexists.EmailId, ifexists.UserId);
                return token;
            }
        }
        private string generatejwttoken(string emailid, long userid)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailId", emailid),
                new Claim("UserId", userid.ToString())
            };
            var token = new JwtSecurityToken(
             issuer:configuration["Jwt:Issuer"],
             audience:configuration["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(15),
             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgetPassword(string Email)
        {
            var EmailCheck=context.UserTable.Where(a=>a.EmailId==Email).FirstOrDefault();
            if (EmailCheck != null)
            {
                var token = generatejwttoken(EmailCheck.EmailId, EmailCheck.UserId);
                new MSMQ().SendMessage(token, EmailCheck.EmailId, EmailCheck.FirstName);
                return token;
            }
            else
            {
                return null;
            }
        }
        public string ResetPassword(ResetPassword reset,string Email)
        {
            try
            {
                if(reset.Password.Equals(reset.ConfirmPassword))
                {
                    var EmailCheck=context.UserTable.Where(u=> u.EmailId==Email).FirstOrDefault();
                    EmailCheck.Password = EncryptPassword(reset.Password);
                    context.SaveChanges();
                    return "Reset Success";
                }
                else
                {
                    return "password does not match";
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
