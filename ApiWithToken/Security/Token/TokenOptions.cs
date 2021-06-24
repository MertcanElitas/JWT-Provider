using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Security.Token
{
    public class TokenOptions
    {
        //Jwt nı dinleyecek olan clientlar.
        public string Audience { get; set; }
        //Jwt yi yayınlayacak olan server.
        public string Issuer { get; set; }
        //Token geçerlilik süresi
        public int AccessTokenExpiration { get; set; }
        //Refresh Token Geçerlilik Süresi
        public int RefreshTokenExpiration { get; set; }

        //Jwt oluşturulurken kullanılack olan anahtar değer.
        public string SecurityKey { get; set; }
    }
}
