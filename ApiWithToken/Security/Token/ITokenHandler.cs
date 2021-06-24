using ApiWithToken.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreaeteAccessToken(Users user);

        void RemoveRefreshToken(Users user); 
    }
}
