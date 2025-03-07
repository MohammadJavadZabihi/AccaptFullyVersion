﻿using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.DataLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies.Interface
{
    public interface IUserServies
    {
        #region User Account Servies

        Task<bool> LoginUser(UserLoginViewModel user);
        Task<User?> RegisterUser(UserRegisterViewModel user);
        Task<User?> UpdateUser(UserUpdateAccountViewModel user);
        Task<List<User>> GetAllUser();
        public Task Save();

        #endregion

        #region Find User Servies

        Task<bool> IsExistEmailAddress(string email);
        Task<bool> IsExistUserName(string username);
        Task<User?> FindeUserByeUserName(string username);
        Task<User?> FindeUSerByActiveCode(string activeCode);
        Task<User?> FindeUSerByEmail(string email);
        Task<InformationUserViewModel> GetUserInfo(string username);
        Task<User?> GetUserById(int id);

        #endregion
    }
}
