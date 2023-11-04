﻿using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);

        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
