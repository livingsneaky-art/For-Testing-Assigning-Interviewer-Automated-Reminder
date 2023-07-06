﻿using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Data.Repositories;
using Basecode.Data.ViewModels;
using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Basecode.Services.Services
{
    public class UserService : ErrorHandling, IUserService
    {
        private readonly IUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>
        /// A list of UserViewModel objects representing all users available.
        /// </returns>
        public List<UserViewModel> RetrieveAll()
        {
            var data = _repository.RetrieveAll().Select(s => new UserViewModel
            {
                Id = s.Id,
                Fullname = s.Fullname,
                Username = s.Username,
                Email = s.Email,
                Role = s.Role,
            }).ToList();

            return data;
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <response code="400">User details are invalid</response>
        public LogContent Create(User user)
        {
            LogContent logContent = new LogContent();

            // Check if the inputted email has a domain (ex: .com, .dev)
            string emailPattern = @"@[^\s@]+\.[^\s@]+$";
            Match match = Regex.Match(user.Email, emailPattern);

            if (!match.Success)
            {
                logContent.Result = true;
                logContent.ErrorCode = "400";
                logContent.Message = "Email address does not have a domain.";
            }
            else
            {
                _repository.Create(user);
            }
            return logContent;
        }

        /// <summary>
        /// Retrieves a specific user based on the provided ID.
        /// </summary>
        /// <param name="id">Represents the ID of the user to fetch.</param>
        /// <returns>
        /// A User object corresponding to the matching ID.
        /// </returns>
        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">Represents the user with updated information.</param>
        public void Update(User user)
        {
            var userToBeUpdated = _repository.GetById(user.Id);
            userToBeUpdated.Fullname = user.Fullname;
            userToBeUpdated.Username = user.Username;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.Role = user.Role;
            _repository.Update(userToBeUpdated);
        }

        /// <summary>
        /// Deletes a user from the system based on the provided ID.
        /// </summary>
        /// <param name="id">Represents the ID of the user to be deleted.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}