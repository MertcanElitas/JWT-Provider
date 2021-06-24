using ApiWithToken.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Responses
{
    public class ProductListResponse : BaseResponse<IEnumerable<Products>>
    {
        private ProductListResponse(bool _isSuccess, string _message, IEnumerable<Products> products) : base(products, _isSuccess, _message)
        {
        }

        //Success
        public ProductListResponse(IEnumerable<Products> products) : this(true, string.Empty, products)
        {
        }

        public ProductListResponse(string message) : this(false, message, null)
        {
        }
    }
}
