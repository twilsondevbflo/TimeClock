using System;
using TimeClock.Models;

namespace TimeClock.Services
{
    /// <summary>
    /// The shift service class handles shift operations (start/end) for a user
    /// </summary>
    public class ShiftService : IShiftService
    {
        private readonly ILogging _logging;
        private readonly DateTime maxDate = new DateTime(9999, 12, 31);

        public ShiftService(ILogging logging)
        {
            _logging = logging;
        }
        /// <summary>
        /// checks if user is allowed to start a new shift 
        /// starting shift resturns a change results object with a status of Active
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckStartShift(User user)
        {
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            };
            System.DateTime tStamp = System.DateTime.Now;
            if (user.Status == UserStatus.Offline)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = tStamp.ConvertDateToString();
                result.ShiftEnd = maxDate.ConvertDateToString();
                result.NewStatus = UserStatus.Active;
                result.Message = $"Shift Started {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";
            }
            else if (user.Status == UserStatus.Active || user.Status == UserStatus.Break || user.Status == UserStatus.Lunch)
            {
                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Active}";
            }

            _logging.WriteHistoryRecord(result);
            return result;


        }
        /// <summary>
        /// checks if user can end a shift
        /// starting break resturs a change results object with a status of Offline
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckEndShift(User user)
        {
            System.DateTime tStamp = System.DateTime.Now;
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            };

            if (user.Status == UserStatus.Offline || user.Status == UserStatus.Break || user.Status == UserStatus.Lunch)
            {
                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Offline}";
            }
            else if (user.Status == UserStatus.Active)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = tStamp.ConvertDateToString();
                result.NewStatus = UserStatus.Offline;
                result.Message = $"Shift Ended {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";
            }

            _logging.WriteHistoryRecord(result);
            return result;
        }
        /// <summary>
        /// checks if user can start a break
        /// starting break resturs a change results object with a status of Break
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckStartBreak(User user)
        {
            System.DateTime tStamp = System.DateTime.Now;
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            }; ;

            if (user.Status == UserStatus.Active)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = tStamp.ConvertDateToString();
                result.ShiftEnd = maxDate.ConvertDateToString();
                result.NewStatus = UserStatus.Break;

                result.Message = $"Break Started {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";

                result.Id = user.Id;
            }
            else if (user.Status == UserStatus.Break || user.Status == UserStatus.Offline || user.Status == UserStatus.Lunch)
            {
                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.Id = user.Id;
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Active}";
            }

            _logging.WriteHistoryRecord(result);
            return result;

        }
        /// <summary>
        /// checks if user is currently in a break and can end the break
        /// ending a break results in setting the user status to active
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckEndBreak(User user)
        {
            System.DateTime tStamp = System.DateTime.Now;
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            };
            if (user.Status == UserStatus.Offline || user.Status == UserStatus.Active || user.Status == UserStatus.Lunch)
            {

                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.Id = user.Id;
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Offline}";

            }
            else if (user.Status == UserStatus.Break)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = tStamp.ConvertDateToString();
                result.NewStatus = UserStatus.Offline;
                result.Message = $"Break Ended {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";
                result.Id = user.Id; ;
            }

            _logging.WriteHistoryRecord(result);
            return result;
        }
        /// <summary>
        /// checks if user is currently allowed to start a lunch shift
        /// starting a lunch results in setting the user status to lunch
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckStartLunch(User user)
        {
            System.DateTime tStamp = System.DateTime.Now;
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            };
            if (user.Status == UserStatus.Active)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = tStamp.ConvertDateToString();
                result.ShiftEnd = maxDate.ConvertDateToString();
                result.NewStatus = UserStatus.Lunch;
                result.Message = $"Lunch Started {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";
            }
            else if (user.Status == UserStatus.Break || user.Status == UserStatus.Offline || user.Status == UserStatus.Lunch)
            {
                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Active}";
            }

            _logging.WriteHistoryRecord(result);
            return result;

        }
        /// <summary>
        /// checks if user is currently in a lunch and ends the lunch 
        /// ending a lunch results in setting the user status to active
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public StatusChangeResult CheckEndLunch(User user)
        {
            System.DateTime tStamp = System.DateTime.Now;
            StatusChangeResult result = new StatusChangeResult()
            {
                OldStatus = user.Status,
                Id = user.Id
            };

            if (user.Status == UserStatus.Offline || user.Status == UserStatus.Active || user.Status == UserStatus.Break)
            {

                result.ChangedStatus = false;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = user.ShiftEnd.ConvertDateToString();
                result.NewStatus = user.Status;
                result.Message = $"Cannot change status from {user.Status} to {UserStatus.Offline}";
            }
            else if (user.Status == UserStatus.Lunch)
            {
                result.ChangedStatus = true;
                result.StatusChangeTime = tStamp.ConvertDateToString();
                result.ShiftStart = user.ShiftStart.ConvertDateToString();
                result.ShiftEnd = tStamp.ConvertDateToString();
                result.NewStatus = UserStatus.Active;
                result.Message = $"Lunch Ended {tStamp.ToString("MMM dd yyyy hh:mm:ss tt")}";
            }

            _logging.WriteHistoryRecord(result);
            return result;
        }
    }
}
