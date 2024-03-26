using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Services.Abstractions;
using Infraestructure.Shared;


namespace Core.Services
{
    public sealed class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repositoryManager.Roles.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<RoleDto>>(categories);
        }

        public async Task<RoleDto> GetByIdAsync(int roleId, CancellationToken cancellationToken = default)
        {
            var role = await _repositoryManager.Roles.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new RoleNotFoundExceptions(roleId);
            }
            return _mapper.Map<RoleDto>(role);
        }
        public async Task<RoleDto> CreateAsync(RoleDto roleForCreationDto, CancellationToken cancellationToken = default)
        {
            var role = _mapper.Map<Role>(roleForCreationDto);

            // Llama al método para insertar la rol utilizando el procedimiento almacenado
            await _repositoryManager.Roles.AddAsync(role);

            return _mapper.Map<RoleDto>(role);
        }



        public async Task UpdateAsync(int roleId, RoleDto roleForUpdateDto, CancellationToken cancellationToken = default)
        {
            // Obtener la rol de la base de datos
            var role = await _repositoryManager.Roles.GetByIdAsync(roleId, cancellationToken);

            // Verificar si la rol
            if (role == null)
            {
                throw new RoleNotFoundExceptions(roleId);
            }

            try
            {

                // Actualizar los campos del rol con la información proporcionada en la DTO
                role.RoleName = roleForUpdateDto.RoleName;
        
                // Guardar los cambios en la base de datos
                await _repositoryManager.Roles.UpdateAsync(role, cancellationToken);

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine($"An error occurred while updating the role: {ex.Message}");
                throw; // por implement excepción para que sea manejada en un nivel superior si es necesario.
            }
        }

        public async Task DeleteAsync(int roleId, CancellationToken cancellationToken = default)
        {
            // Obtener el rol por su ID
            var role = await _repositoryManager.Roles.GetByIdAsync(roleId, cancellationToken);

            // Verificar si el rol existe
            if (role == null)
            {
                throw new RoleNotFoundExceptions(roleId);
            }

            // Llamar al método RemoveAsync del repositorio para eliminar el rol
            await _repositoryManager.Roles.RemoveAsync(role, cancellationToken);
        }

    }

}
