using System.Collections.Generic;
using TimeClock.Models;

namespace TimeClock
{
    public static class MockData
    {
        public static IEnumerable<User> GetUserDB()
        {
            return new List<User>()
            {
                new User() { Id = 1, Status = UserStatus.Offline},
                new User() { Id = 2, Status = UserStatus.Offline}
            };
        }
    }
}
