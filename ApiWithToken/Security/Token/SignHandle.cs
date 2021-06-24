using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithToken.Security.Token
{
    public static class SignHandle
    {
        public static SecurityKey GetSecurityKey(string securityKey)
        {
            //Tokenın hem oluşturma hemde doğrulama işleminde aynı securityKey değerini kullanacağımız için SymetricSecuırityKey Kullandık.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
