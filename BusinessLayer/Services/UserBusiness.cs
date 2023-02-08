﻿using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepository user;
        public UserBusiness(IUserRepository user)
        {
            this.user = user;
        }
        public UserEntity Register(RegisterModel model)
        {
            return user.Register(model);
        }
        public string Login(LoginModel login)
        {
            return user.Login(login);
        }
        public string ForgetPassword(string Email)
        {
            return user.ForgetPassword(Email);
        }
        public string ResetPassword(ResetPassword reset, string Email)
        {
            return user.ResetPassword(reset, Email);
        }
    }
}
