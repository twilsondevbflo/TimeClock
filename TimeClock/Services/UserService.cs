using System;
using System.Collections.Generic;
using System.Linq;
using TimeClock.Models;

namespace TimeClock.Services
{
    /// <summary>
    /// the user service class handles user operations such as beginning/ending shifts and determining if usrs are valid
    /// and returning user history
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IShiftService _shiftService;
        private readonly ILogging _logger;
        public UserService(IShiftService shiftService, ILogging logger)
        {
            _shiftService = shiftService;
            _logger = logger;
        }

        public IEnumerable<User> GetUsers()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// this returns the user history based on userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<StatusChangeResult> GetUserHistory(int userId)
        {
            return _logger.ReadHistory(userId);
        }
        public bool IsUser(int userId)
        {
            //logic to see if the user exists by Id
          

            if (MockData.GetUserDB().Any(x => x.Id == userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public User StartShift(User user , UserStatus startShiftType)
        {
            StatusChangeResult result = startShiftType switch
            {
                UserStatus.Active => _shiftService.CheckStartShift(user),
                UserStatus.Break => _shiftService.CheckStartBreak(user),
                UserStatus.Lunch => _shiftService.CheckStartBreak(user),
                _ => throw new NotImplementedException()

            };
            
            if (result.ChangedStatus)
            {
                UpdateUser(result, user);

            }
            else
            {
                user.Message = result.Message;
            }
            return user;
        }
        public User EndShift(User user, UserStatus endShiftType)
        {
            StatusChangeResult result = endShiftType switch
            {
                UserStatus.Active => _shiftService.CheckEndShift(user),
                UserStatus.Break => _shiftService.CheckEndBreak(user),
                UserStatus.Lunch => _shiftService.CheckEndLunch(user),
                _ => throw new NotImplementedException()

            };

            if (result.ChangedStatus)
            {
                UpdateUser(result, user);

            }
            else
            {
                user.Message = result.Message;
            }
            return user;
        }
        private void UpdateUser(StatusChangeResult result, User user)
        {
            user.Status = result.NewStatus;
            user.ShiftStart = result.ShiftStart.ConvertStringToDate();
            user.ShiftEnd = result.ShiftEnd.ConvertStringToDate();
            user.Message = result.Message;
        }
      
    }
}
