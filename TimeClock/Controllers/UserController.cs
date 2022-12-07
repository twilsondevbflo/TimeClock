using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TimeClock.Models;
using TimeClock.Services;

namespace TimeClock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userSerivce, IShiftService shiftService)
        {
            _userService = userSerivce;
        }

        [HttpGet("User/Login")]
        public string Login(int userId)
        {
            if (_userService.IsUser(userId))
            {
                return $"User {userId} logged in";
            }
            else
            {
                return $"Could Not Login with Id {userId}";
            }
        }

        [HttpGet("User/GetHistory")]
        public IEnumerable<StatusChangeResult> GetHistory(int userId)
        {
            return _userService.GetUserHistory(userId);
        }

        [HttpPost("User/StartShift")]
        public User StartShift([FromBody] User user)
        {
            user = _userService.StartShift(user, UserStatus.Active);
            return user;
        }

        [HttpPost("User/EndShift")]
        public User EndShift([FromBody] User user)
        {
            return _userService.EndShift(user, UserStatus.Active);
        }

        [HttpPost("User/StartBreak")]
        public User StartBreak([FromBody] User user)
        {
            user = _userService.StartShift(user, UserStatus.Break);
            return user;
        }

        [HttpPost("User/EndBreak")]
        public User EndBreak([FromBody] User user)
        {
            return _userService.EndShift(user, UserStatus.Break);
        }

        [HttpPost("User/StartLunch")]
        public User StartLunch([FromBody] User user)
        {
            user = _userService.StartShift(user, UserStatus.Lunch);
            return user;
        }

        [HttpPost("User/EndLunch")]
        public User EndLunch([FromBody] User user)
        {
            return _userService.EndShift(user, UserStatus.Lunch);
        }
    }
}
