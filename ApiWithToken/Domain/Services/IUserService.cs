using ApiWithToken.Domain.Model;
using ApiWithToken.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IUserService
    {
        UserResponse AddUser(Users user);

        UserResponse FindById(int userId);

        UserResponse FindByEmailAndPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        UserResponse GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(Users user);
    }
}
