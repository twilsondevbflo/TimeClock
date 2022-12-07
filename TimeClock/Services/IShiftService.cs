using TimeClock.Models;

namespace TimeClock.Services
{
    public interface IShiftService
    {
        StatusChangeResult CheckStartShift(User user);
        StatusChangeResult CheckEndShift(User user);
        StatusChangeResult CheckStartBreak(User user);
        StatusChangeResult CheckEndBreak(User user);
        StatusChangeResult CheckStartLunch(User user);
        StatusChangeResult CheckEndLunch(User user);
    }
}
