using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Helpers;
using Domain.Repositories;
using Domain.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Domain.Services
{
    public sealed class PersonService : IPersonService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public PersonService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }


        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken = default)
        {
            // Verificar las credenciales del usuario en la base de datos
            var user = await _repositoryManager.People.SearchPersonByCredentialsAsync(model.UserName, model.Pass, cancellationToken);

            // Retornar null si el usuario no fue encontrado
            if (user == null) return null;

            // Obtener el tipo de rol del usuario
            var role = user.RoleId; // Supongamos que RoleId es el campo que almacena el tipo de rol del usuario

            // Generar el token JWT incluyendo el tipo de rol
            var token = GenerateJwtToken(user, role);

            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(Person user, int roleId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("id", user.Id.ToString()),
            new Claim("role_id", roleId.ToString()) // Incluir el tipo de rol como claim en el token
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var people = await _repositoryManager.People.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public async Task<PersonDto> GetByIdAsync(int personId, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.People.GetByIdAsync(personId, cancellationToken);
            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }
            return _mapper.Map<PersonDto>(person);
        }
        public async Task<PersonDto> CreateAsync(PersonDto PersonForCreationDto, CancellationToken cancellationToken = default)
        {
            var person = _mapper.Map<Person>(PersonForCreationDto);

            // Llama al método para insertar lapersona utilizando el procedimiento almacenado
            await _repositoryManager.People.AddAsync(person);

            return _mapper.Map<PersonDto>(person);
        }



        public async Task UpdateAsync(int personId, PersonDto personForUpdateDto, CancellationToken cancellationToken = default)
        {
            // Obtener lapersona de la base de datos
            var person = await _repositoryManager.People.GetByIdAsync(personId, cancellationToken);

            // Verificar si lapersona
            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }

            try
            {

                // Actualizar los campos de la persona con la información proporcionada en la DTO
                person.UserName = personForUpdateDto.UserName;
                person.Pass = personForUpdateDto.Pass;
                person.Nombre = personForUpdateDto.Nombre;
                person.Apellido = personForUpdateDto.Apellido;
                person.Email = personForUpdateDto.Email;
                person.DepartmentId = personForUpdateDto.DepartmentId;
                person.RoleId = personForUpdateDto.RoleId;

                // Guardar los cambios en la base de datos
                await _repositoryManager.People.UpdateAsync(person, cancellationToken);

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine($"An error occurred while updating the person: {ex.Message}");
                throw; // por implement excepción para que sea manejada en un nivel superior si es necesario.
            }
        }

        public async Task DeleteAsync(int personId, CancellationToken cancellationToken = default)
        {
            // Obtener lapersona por su ID
            var person = await _repositoryManager.People.GetByIdAsync(personId, cancellationToken);

            // Verificar si lapersona existe
            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }

            // Llamar al método RemoveAsync del repositorio para eliminar lapersona
            await _repositoryManager.People.RemoveAsync(person, cancellationToken);
        }

    }
    

}
