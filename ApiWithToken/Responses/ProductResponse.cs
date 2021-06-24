using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWithToken.Domain.Model;

namespace ApiWithToken.Responses
{
    public class ProductResponse : BaseResponse<Products>
    {
        private ProductResponse(bool _isSuccess, string _message, Products product) : base(product, _isSuccess, _message)
        {
        }

        //Success
        public ProductResponse(Products product) : this(true, string.Empty, product)
        {
        }


        public ProductResponse(string message) : this(false, message, null)
        {
        }
    }
}
