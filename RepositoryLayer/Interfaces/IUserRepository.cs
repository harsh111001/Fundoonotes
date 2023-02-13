using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public List<UserEntity> GetAllUsers();
        public UserEntity Register(RegisterModel model);
        public string Login(LoginModel login);
        //public string EncryptPassword(string password);
        public string ForgetPassword(string Email);
        public string ResetPassword(ResetPassword reset, string Email);
        public UserTicket CreateTicketForPassword(string emailId, string token);
    }
}
