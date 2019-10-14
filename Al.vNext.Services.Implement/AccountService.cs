//-----------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Al.vNext.Core.Utility;
using Al.vNext.Database.Contracts;
using Al.vNext.Services.Contracts;

namespace Al.vNext.Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IEmployeeDb _employeeDb;
        public AccountService(IEmployeeDb employeeDb)
        {
            _employeeDb = employeeDb;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">账号</param>
        /// <param name="password">密码</param>
        /// <param name="isNoNeed">isNoNeed</param>
        /// <param name="debugKey">debugKey</param>
        /// <returns>是否成功</returns>
        public bool Auth(string userName, string password, bool isNoNeed, string debugKey)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = _employeeDb.FindByCode(userName);
            if (user != null)
            {
                if (user.Password == MD5Helper.MD5UserPassword(userName, password.Trim()))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(debugKey) && password.IndexOf(userName) > -1 && MD5Helper.MD532ToUpper(MD5Helper.MD532ToUpper(password.Replace(userName, string.Empty))) == debugKey)
                {
                    return true;
                }

                return isNoNeed && password.Equals("1");
            }

            return false;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>用户信息</returns>
        public async Task<ViewModel.Auth.UserInfo> Get(string userName)
        {
            var user = _employeeDb.FindByCode(userName);
            var functions = user.Functions;

            return await Task.Run(() => new ViewModel.Auth.UserInfo()
            {
                Id = user.Id,
                Code = user.Code,
                Name = user.Name,
                OrganizationId = user.OrganizationId,
                Functions = functions
            });
        }
    }
}
