using System.Collections.Generic;
using TimeClock.Models;

namespace TimeClock.Services
{
    public interface IUserService
    {
        public bool IsUser(int userId);
        IEnumerable<StatusChangeResult> GetUserHistory(int userId);
        public IEnumerable<User> GetUsers();
        public User StartShift(User user, UserStatus startShiftType);
        public User EndShift(User user, UserStatus endShiftType);        
    }
}
