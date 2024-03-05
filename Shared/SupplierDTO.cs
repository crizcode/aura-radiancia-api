using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Estado { get; set; }


        // Constructor vacío necesario para serialización/deserialización
        public SupplierDto()
        {
        }

        // Constructor para inicializar los campos del DTO
        public SupplierDto(int supplierId, string name, string estado)
        {
            SupplierId = supplierId;
            Name = name;
            Estado = estado;
        }
    }
}