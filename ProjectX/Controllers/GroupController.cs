using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : MainController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult GetFaculties()
        {
            try
            {
                var faculties = _groupService.GetFaculties();
                return Ok(faculties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
