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
    public class WalletServies : IWalletServies
    {
        private readonly AccaptContext _context;
        private readonly IUserServies _userServies;

        public WalletServies(AccaptContext context, IUserServies userServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
        }

        public async Task<Wallet?> AddFoundWallet(int amount, string userName)
        {
            var wallet = await FindeWalletWithUserName(userName);

            if (wallet == null)
                return null;

            wallet.Amount += amount;

            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task<object> AddWallet(int userId)
        {
            var existUser = await _userServies.GetUserById(userId);
            if(existUser != null)
            {
                try
                {
                    Wallet wallet = new Wallet()
                    {
                        Amount = 0,
                        UserId = userId,
                    };

                    await _context.AddAsync(wallet);
                    await _context.SaveChangesAsync();

                    return wallet;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            return null;
        }

        public async Task<Wallet?> FindeWalletWithUserName(string userName)
        {
            var user = await _userServies.FindeUserByeUserName(userName);

            if (user == null)
                return null;

            return await _context.Wallet.FirstOrDefaultAsync(w => w.UserId == user.UserId);
        }
    }
}
