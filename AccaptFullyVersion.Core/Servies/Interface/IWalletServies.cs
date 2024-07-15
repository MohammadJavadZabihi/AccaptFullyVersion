using AccaptFullyVersion.DataLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies.Interface
{
    public interface IWalletServies
    {
        Task<object> AddWallet(int userId);
        Task<Wallet?> FindeWalletWithUserName(string userName);
        Task<Wallet?> AddFoundWallet(int amount, string userName);
    }
}
