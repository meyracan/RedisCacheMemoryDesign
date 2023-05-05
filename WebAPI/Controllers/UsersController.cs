using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public void Add(User user)
        {
             _userService.Add(user);
        }

        [HttpDelete("delete")]
        public void Delete(User user)
        {
            _userService.Delete(user);
        }

        [HttpPut("update")]
        public void Update(User user)
        {
            _userService.Update(user);
        }

        [HttpGet("getall")]
        public List<User> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpGet("getbyid")]
        public User GetById(int Id)
        {
            return _userService.GetById(Id);
        }
    }
}
