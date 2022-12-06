using System.Collections.Generic;

namespace TimeClock.Services
{
    public interface ILogging
    {
        IEnumerable<StatusChangeResult> ReadHistory(int userId);
        void WriteHistoryRecord(StatusChangeResult output);
    }
}
