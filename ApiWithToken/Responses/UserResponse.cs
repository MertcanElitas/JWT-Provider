using ApiWithToken.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Responses
{
    public class UserResponse : BaseResponse<Users>
    {

        public UserResponse(Users User, bool _isSuccess, string _message) : base(User, _isSuccess, _message)
        {
        }

        public UserResponse(Users User) : this(User, true, string.Empty)
        {

        }

        public UserResponse(string message) : this(null, false, message)
        {

        }
    }
}
