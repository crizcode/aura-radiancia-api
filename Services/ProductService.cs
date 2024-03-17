using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services.Abstractions;
using Infraestructure.Shared;

namespace Domain.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _repositoryManager.Products.GetAllAsync(cancellationToken);

            // Cargar las entidades relacionadas (Category y Supplier) para cada producto
            foreach (var product in products)
            {
                if (product.CategoryId != 0)
                {
                    product.Category = await _repositoryManager.Categories.GetByIdAsync(product.CategoryId, cancellationToken);
                }

                if (product.SupplierId != 0)
                {
                    product.Supplier = await _repositoryManager.Suppliers.GetByIdAsync(product.SupplierId, cancellationToken);
                }
            }

            // Mapear los datos a ProductDto
            var productDtos = products.Select(product => _mapper.Map<ProductDto>(product));

            return productDtos;
        }

        public async Task<ProductDto> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            var product = await _repositoryManager.Products.GetByIdAsync(productId, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundExceptions(productId);
            }

            // Cargar las entidades relacionadas (Category y Supplier) para el producto
            if (product.CategoryId != 0)
            {
                product.Category = await _repositoryManager.Categories.GetByIdAsync(product.CategoryId, cancellationToken);
            }

            if (product.SupplierId != 0)
            {
                product.Supplier = await _repositoryManager.Suppliers.GetByIdAsync(product.SupplierId, cancellationToken);
            }

            // Mapear los datos a ProductDto
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }


        public async Task<ProductDto> CreateAsync(ProductDto productForCreationDto, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(productForCreationDto);

            // Llama al método para insertar el producto utilizando el procedimiento almacenado
            await _repositoryManager.Products.AddAsync(product);

            return _mapper.Map<ProductDto>(product);
        }



        public async Task UpdateAsync(int productId, ProductDto productForUpdateDto, CancellationToken cancellationToken = default)
        {
            // Obtener el producto de la base de datos
            var product = await _repositoryManager.Products.GetByIdAsync(productId, cancellationToken);

            // Verificar si el producto existe
            if (product == null)
            {
                throw new ProductNotFoundExceptions(productId);
            }

            try
            {

                // Actualizar los campos del producto con la información proporcionada en el DTO
            product.Name = productForUpdateDto.Name;
            product.Descripcion = productForUpdateDto.Descripcion;
            product.Precio = productForUpdateDto.Precio;
            product.Stock = productForUpdateDto.Stock;
            product.CreationDate = productForUpdateDto.CreationDate;
            product.CategoryId = productForUpdateDto.CategoryId;
            product.SupplierId = productForUpdateDto.SupplierId;

            // Guardar los cambios en la base de datos
            await _repositoryManager.Products.UpdateAsync(product, cancellationToken);
   
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
                throw; // por implement excepción para que sea manejada en un nivel superior si es necesario.
            }
        }

        public async Task DeleteAsync(int productId, CancellationToken cancellationToken = default)
        {
            // Obtener el producto por su ID
            var product = await _repositoryManager.Products.GetByIdAsync(productId, cancellationToken);

            // Verificar si el producto existe
            if (product == null)
            {
                throw new ProductNotFoundExceptions(productId);
            }

            // Llamar al método RemoveAsync del repositorio para eliminar el producto
            await _repositoryManager.Products.RemoveAsync(product, cancellationToken);
        }

    }

}
