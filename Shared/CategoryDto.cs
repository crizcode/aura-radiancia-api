
namespace Infraestructure.Shared
{
    public class CategoryDto
     {
            public int CategoryId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Estado { get; set; } = string.Empty;
   

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