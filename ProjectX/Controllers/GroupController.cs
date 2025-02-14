using Application.DTOs;
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

        [HttpGet]
        public IActionResult GetAllGroups()
        {
            try
            {
                var groups = _groupService.GetGroups();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult GetGroup(GroupDTO group)
        {
            try
            {
                var groupToReturn = _groupService.GetGroupByGroupNumber(group);
                return Ok(groupToReturn);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddGroup(GroupDTO group)
        {
            try
            {
                _groupService.AddGroup(group);
                return StatusCode(StatusCodes.Status201Created, "Group was added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteGroup(GroupDTO group)
        {
            try
            {
                if(group.Id  == 0) return StatusCode(StatusCodes.Status400BadRequest, "Group Id is not valid");
                _groupService.DeleteGroup(group);
                return StatusCode(StatusCodes.Status200OK, "Group was deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
