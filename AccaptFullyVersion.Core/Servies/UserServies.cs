using AccaptFullyVersion.Core.Convertor;
using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Generator;
using AccaptFullyVersion.Core.Secutiry;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Context;
using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies
{
    public class UserServies : IUserServies
    {
        private readonly AccaptContext _context;
        public UserServies(AccaptContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public Task<User?> FindeUSerByActiveCode(string activeCode)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> FindeUSerByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> FindeUserByeUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<InformationUserViewModel> GetUserInfo(string email)
        {
            var user = await FindeUSerByEmail(email);
            if(user !=null)
            {
                InformationUserViewModel info = new InformationUserViewModel();
                info.Email = user.Email;
                info.UserName = user.UserName;
                info.Wallet = 0;
            }
        }

        public async Task<bool> IsExistEmailAddress(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsExistUserName(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> LoginUser(UserLoginViewModel user)
        {
            if(user != null)
            {
                string hashPass = PasswordHelper.EncodePasswordMd5(user.Password);
                return await _context.Users.AnyAsync(u => u.Email == user.Email && u.Password == hashPass);
            }

            return false;
        }

        public async Task<bool> RegisterUser(UserRegisterViewModel user)
        {
            try
            {
                if(user != null)
                {
                    User eUser = new User
                    {
                        UserName = user.UserName,
                        Password = PasswordHelper.EncodePasswordMd5(user.Password),
                        ActiveCode = NameGenerator.GenerateUniqCode(),
                        Email = FixedText.FixedEmailTex(user.Email),
                        RegisterDate = DateTime.UtcNow,
                        IsActive = false
                    };

                    await _context.AddAsync(eUser);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(UserUpdateAccountViewModel user)
        {
            try
            {
                if(user != null)
                {
                    User eUser = new User();

                    eUser.UserName = user.UserName;
                    eUser.Email = user.Email;

                    _context.Users.Update(eUser);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
