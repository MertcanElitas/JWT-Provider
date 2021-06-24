using ApiWithToken.Domain.Context;
using ApiWithToken.Domain.Model;
using ApiWithToken.Domain.Repositories.Interfaces;
using ApiWithToken.Security.Token;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories.Concrete
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly TokenOptions _tokenOptions;
        
        public UserRepository(ApiWihTokenDbContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public void AddUser(Users user)
        {
            _context.Users.Add(user);
        }

        public Users FindByEmailAndPassword(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
                throw new ArgumentException("Model Cannot Be Null");

            return user;
        }

        public Users FindById(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
                throw new ArgumentException("Model Cannot Be Null");

            return user;
        }

        public Users GetUserWithRefreshToken(string refreshToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if (user == null)
                throw new ArgumentException("Model Cannot Be Null");

            return user;
        }

        public void RemoveRefreshToken(Users user)
        {
            var model = FindById(user.Id);

            if (model == null)
                throw new ArgumentException("Model Cannot Be Null");

            model.RefreshToken = null;
            model.RefreshTokenEndDate = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            var model = FindById(userId);

            if (model == null)
                throw new ArgumentException("Model Cannot Be Null");

            model.RefreshToken = refreshToken;
            model.RefreshTokenEndDate = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
        }
    }
}
