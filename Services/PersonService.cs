using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Helpers;
using Core.Domain.Interfaces;
using Core.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.Extensions.Options;


namespace Core.Services
{
    public sealed class PersonService : IPersonService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            await _repositoryManager.People.AddAsync(person);

            return _mapper.Map<PersonDto>(person);
        }



        public async Task UpdateAsync(int personId, PersonDto personForUpdateDto, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.People.GetByIdAsync(personId, cancellationToken);

            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }

            try
            {
                person.UserName = personForUpdateDto.UserName;
                person.Pass = personForUpdateDto.Pass;
                person.Nombre = personForUpdateDto.Nombre;
                person.Apellido = personForUpdateDto.Apellido;
                person.Email = personForUpdateDto.Email;
                person.DepartmentId = personForUpdateDto.DepartmentId;
                person.RoleId = personForUpdateDto.RoleId;

                await _repositoryManager.People.UpdateAsync(person, cancellationToken);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int personId, CancellationToken cancellationToken = default)
        {
            var person = await _repositoryManager.People.GetByIdAsync(personId, cancellationToken);

            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }

            await _repositoryManager.People.RemoveAsync(person, cancellationToken);
        }

    }
    

}
