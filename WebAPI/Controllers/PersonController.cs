using Domain.Entities;
using Domain.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/v1/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public PersonController(IServiceManager serviceManager) => _serviceManager = serviceManager;


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _serviceManager.PersonService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Usuario o contraseña incorrecto" });

            return Ok(response);
        }

        // List People
        [HttpGet("list")]
        public async Task<IActionResult> GetPeople(CancellationToken cancellationToken)
        {
            try
            {
                var people = await _serviceManager.PersonService.GetAllAsync(cancellationToken);
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error al listar las personas: " + ex.Message });
            }
        }

        // List Person by Id

        [HttpGet("list/{personId}")]
        public async Task<IActionResult> GetPersonById(int personId, CancellationToken cancellationToken)
        {

            try
            {
                var personDto = await _serviceManager.PersonService.GetByIdAsync(personId, cancellationToken);
                return Ok(personDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Error al listar la persona " + ex.Message});
            }
        }


        // Add Person

        [AllowAnonymous]
        [HttpPost("save")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto PersonForCreationDto)
        {
            try
            {
                await _serviceManager.PersonService.CreateAsync(PersonForCreationDto);

                return Ok(new { message = "Persona creada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al crear la persona " + ex.Message });
            }
        }



        // Update Person
        [HttpPut("update/{personId}")]
        public async Task<IActionResult> UpdatePerson(int personId, [FromBody] PersonDto PersonForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.PersonService.UpdateAsync(personId, PersonForUpdateDto, cancellationToken);

                return Ok(new { message = "Persona actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar la persona " + ex.Message });
            }
        }


        // Delete Person
        [HttpDelete("delete/{personId}")] 
        public async Task<IActionResult> DeletePerson(int personId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.PersonService.DeleteAsync(personId, cancellationToken);

                return Ok(new { message = "Persona eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar persona " + ex.Message });
            }
        }
    }


}