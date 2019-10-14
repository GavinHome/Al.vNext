//-----------------------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Al.vNext.ViewModel.Auth;

namespace Al.vNext.Services.Contracts
{
    public interface IAccountService
    {
        Task<UserInfo> Get(string userName);
        bool Auth(string username, string password, bool isDev, string value);
    }
}
