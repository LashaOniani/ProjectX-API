using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : MainController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson([FromBody] SignUpDTO person)
        {
            if (person == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Person data is requiered");
            }

            try
            {
                if (person.Password != person.RePassword)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, "Passwords aren't same");
                }
                else
                {   
                    await _personService.SavePersonAsync(person);
                    return StatusCode(StatusCodes.Status200OK, "Registration was successful");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetPerson(LoginRequestDTO request)
        {
            var authUser = AuthUser;
            try
            {
                if(authUser != null)
                {

                    return Ok();
                }
                else return StatusCode(StatusCodes.Status404NotFound, "Person is not authorized");
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetUsersByFullName(FindUserDTO fullName)
        {
            try
            {
                if(fullName == null)
                {
                    return BadRequest();
                }
                else
                {
                    List<User> users = _personService.GetUsers(fullName);
                    return Ok(users);
                }
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
