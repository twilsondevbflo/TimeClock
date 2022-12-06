using System;
using FileHelpers;
namespace TimeClock
{
    [DelimitedRecord(",")]
    public class StatusChangeResult
    {
        public int Id { get; set; }
        public string StatusChangeTime { get; set; }
        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public bool ChangedStatus { get; set; }
        public UserStatus OldStatus { get; set; }
        public UserStatus NewStatus { get; set; }
        public string Message { get; set; }
    }
}
