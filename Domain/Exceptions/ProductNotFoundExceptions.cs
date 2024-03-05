using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductNotFoundExceptions : NotFoundException
    {
        public ProductNotFoundExceptions(int productid) 
            : base($"EL producto con id {productid} no fue encontrado.")
        {
        }
    }
}
