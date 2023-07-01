using Microsoft.AspNetCore.Mvc;
using QuickActions.Api;
using Sample.Common.Interfaces;
using Sample.Common.Models;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : CrudController<User>, IUsers
    {
        public UsersController(CrudRepository<User> repository) : base(repository)
        {
        }
    }
}