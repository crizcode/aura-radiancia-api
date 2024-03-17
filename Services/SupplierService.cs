using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services.Abstractions;
using Infraestructure.Shared;

namespace Domain.Services

{
    public sealed class SupplierService : ISupplierService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var suppliers = await _repositoryManager.Suppliers.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetByIdAsync(int supplierId, CancellationToken cancellationToken = default)
        {
            var supplier = await _repositoryManager.Suppliers.GetByIdAsync(supplierId, cancellationToken);
            if (supplier == null)
            {
                throw new SupplierNotFoundExceptions(supplierId);
            }
            return _mapper.Map<SupplierDto>(supplier);
        }
        public async Task<SupplierDto> CreateAsync(SupplierDto supplierForCreationDto, CancellationToken cancellationToken = default)
        {
            var supplier = _mapper.Map<Supplier>(supplierForCreationDto);

            // Llama al método para insertar la proveedor utilizando el procedimiento almacenado
            await _repositoryManager.Suppliers.AddAsync(supplier);

            return _mapper.Map<SupplierDto>(supplier);
        }



        public async Task UpdateAsync(int supplierId, SupplierDto supplierForUpdateDto, CancellationToken cancellationToken = default)
        {
            // Obtener proveedor de la base de datos
            var supplier = await _repositoryManager.Suppliers.GetByIdAsync(supplierId, cancellationToken);

            // Verificar si la categoria
            if (supplier == null)
            {
                throw new SupplierNotFoundExceptions(supplierId);
            }

            try
            {

                // Actualizar los campos de la categoria con la información proporcionada en la DTO
                supplier.Name = supplierForUpdateDto.Name;
                supplier.Estado = supplierForUpdateDto.Estado;


                // Guardar los cambios en la base de datos
                await _repositoryManager.Suppliers.UpdateAsync(supplier, cancellationToken);

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine($"An error occurred while updating the supplier: {ex.Message}");
                throw; // por implement excepción para que sea manejada en un nivel superior si es necesario.
            }
        }

        public async Task DeleteAsync(int supplierId, CancellationToken cancellationToken = default)
        {
            // Obtener la categoria por su ID
            var supplier = await _repositoryManager.Suppliers.GetByIdAsync(supplierId, cancellationToken);

            // Verificar si la categoria existe
            if (supplier == null)
            {
                throw new SupplierNotFoundExceptions(supplierId);
            }

            // Llamar al método RemoveAsync del repositorio para eliminar la categoria
            await _repositoryManager.Suppliers.RemoveAsync(supplier, cancellationToken);
        }

    }

}
