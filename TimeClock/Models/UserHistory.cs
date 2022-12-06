using System;

namespace TimeClock.Models
{
    public class UserHistory
    {
        public int Id { get; set; }
        public UserStatus UserStatus { get; set; }
        public DateTime StatusStart { get; set; }
        public DateTime StatusEnd { get; set; }
    }
}
