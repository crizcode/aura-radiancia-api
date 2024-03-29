
namespace Infraestructure.Shared
{
    public class CategoryDto
     {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public string Descrip { get; set; }
            public string? Estado { get; set; } // 0 o 1

        public CategoryDto()
            {
            }

            public CategoryDto(int categoryid, string name, string descrip, string estado)
            {
                CategoryId = categoryid;
                Name = name;
                Descrip = descrip;
                Estado = estado;
            }
    }
}