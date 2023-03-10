using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public UserEntity Register(RegisterModel model);
        public List<UserEntity> GetAllUsers();
        public string Login(LoginModel login);
        public string ForgetPassword(string Email);
        public string ResetPassword(ResetPassword reset, string Email);
        public UserTicket CreateTicketForPassword(string emailId, string token);
    }
}
