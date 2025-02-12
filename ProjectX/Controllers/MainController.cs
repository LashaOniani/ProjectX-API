using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected Person? AuthUser
        {
            get
            {
                var currentUser = HttpContext.User;

                if (currentUser != null && currentUser.HasClaim(c => c.Type == "UserID"))
                {
                    var userIdClaim = currentUser.FindFirst("UserID");
                    var roleIdClaim = currentUser.FindFirst("RoleID"); // role_ID

                    if (userIdClaim != null)
                    {
                        return new Person
                        {
                            Id = int.Parse(userIdClaim.Value),
                            R_id = int.Parse(roleIdClaim.Value)
                        };
                    }
                }
                return null;
            }
        }
    }
}
