
namespace Infraestructure.Shared
{
    public class RoleDto
     {
            public int Id { get; set; }
            public string RoleName { get; set; }

            public RoleDto()
            {
            }

            public RoleDto(int roleid, string name)
            {
                Id = roleid;
                RoleName = name;
            }
    }
}