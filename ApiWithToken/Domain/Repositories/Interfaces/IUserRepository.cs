using ApiWithToken.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(Users user);

        Users FindById(int userId);

        Users FindByEmailAndPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        Users GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(Users user);
    }
}
