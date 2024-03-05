using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class SupplierNotFoundExceptions : NotFoundException
    {
        public SupplierNotFoundExceptions(int supplierId)
            : base($"La proveedor con id {supplierId} no fue encontrado.")
        {
        }
    }
}