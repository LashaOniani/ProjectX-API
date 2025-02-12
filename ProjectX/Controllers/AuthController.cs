using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly IJwtManager _jwtManager;
        private readonly IPersonService _personService;
        public AuthController(IJwtManager jwtManager, IPersonService personService)
        {
            _jwtManager = jwtManager;
            _personService = personService;
        }

        [HttpPost]
        public IActionResult Authenthicate(LoginRequestDTO loginRequest)
        {
            var person = _personService.GetPerson(loginRequest);
            if (person == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Person not found.");
            }
            var token = _jwtManager.GetToken(person);
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAuthUser()
        {
            var authUser = AuthUser;
            try
            {
                if (authUser != null && authUser.Id > 0)
                {
                    int userId = (int)authUser.Id;
                    var user = _personService.GetPersonById(userId);
                    return Ok(user);
                }
                else return StatusCode(StatusCodes.Status404NotFound, "Person is not authorized");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
