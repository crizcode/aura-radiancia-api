using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CategoryDto
     {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public string Estado{ get; set; }
   

            // Constructor vacío necesario para serialización/deserialización
            public CategoryDto()
            {
            }

            // Constructor para inicializar los campos del DTO
            public CategoryDto(int categoryid, string name, string estado)
            {
                CategoryId = categoryid;
                Name = name;
                Estado = estado;
            }
    }
}