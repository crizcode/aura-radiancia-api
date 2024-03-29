using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Services.Abstractions;
using Infraestructure.Shared;


namespace Core.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repositoryManager.Categories.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _repositoryManager.Categories.GetByIdAsync(categoryId, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundExceptions(categoryId);
            }
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> CreateAsync(CategoryDto categoryForCreationDto, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(categoryForCreationDto);

            // Llama al método para insertar la categoria utilizando el procedimiento almacenado
            await _repositoryManager.Categories.AddAsync(category);

            return _mapper.Map<CategoryDto>(category);
        }



        public async Task UpdateAsync(int categoryId, CategoryDto categoryForUpdateDto, CancellationToken cancellationToken = default)
        {
            // Obtener la categoria de la base de datos
            var category = await _repositoryManager.Categories.GetByIdAsync(categoryId, cancellationToken);

            // Verificar si la categoria
            if (category == null)
            {
                throw new CategoryNotFoundExceptions(categoryId);
            }

            try
            {

                // Actualizar los campos de la categoria con la información proporcionada en la DTO
                category.Name = categoryForUpdateDto.Name;
                category.Descrip = categoryForUpdateDto.Descrip;
                category.Estado = categoryForUpdateDto.Estado;

                // Guardar los cambios en la base de datos
                await _repositoryManager.Categories.UpdateAsync(category, cancellationToken);

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine($"An error occurred while updating the category: {ex.Message}");
                throw; // por implement excepción para que sea manejada en un nivel superior si es necesario.
            }
        }

        public async Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            // Obtener la categoria por su ID
            var category = await _repositoryManager.Categories.GetByIdAsync(categoryId, cancellationToken);

            // Verificar si la categoria existe
            if (category == null)
            {
                throw new CategoryNotFoundExceptions(categoryId);
            }

            // Llamar al método RemoveAsync del repositorio para eliminar la categoria
            await _repositoryManager.Categories.RemoveAsync(category, cancellationToken);
        }

    }

}
