using System;
using System.Collections.Generic;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                throw new IndexOutOfRangeException("Index was less than 0.");

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new KeyNotFoundException("User was not found.");

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("The task already exists");
            }

            tasks.Add(task);
        }
    }
}