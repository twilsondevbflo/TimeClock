using System;

namespace TimeClock.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserStatus Status { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string Message { get; set; }
    }
}
