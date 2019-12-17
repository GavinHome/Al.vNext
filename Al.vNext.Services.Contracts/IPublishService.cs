//-----------------------------------------------------------------------------------
// <copyright file="IPublishService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/12/17 14:27:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Al.vNext.ViewModel.Auth;

namespace Al.vNext.Services.Contracts
{
    public interface IPublishService
    {
        Task<bool> Send<T>(T data, string queneName);
    }
}
