using ApiWithToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Responses
{
    public class AccessTokenResponse : BaseResponse<AccessToken>
    {
        public AccessTokenResponse(AccessToken _accessToken, bool _isSuccess, string _message) : base(_accessToken, _isSuccess, _message)
        {
        }

        public AccessTokenResponse(AccessToken _accessToken) : this(_accessToken, true, string.Empty)
        {
        }

        public AccessTokenResponse(string message) : this(null, false, message)
        {
        }
    }
}
