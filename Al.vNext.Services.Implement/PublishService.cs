//-----------------------------------------------------------------------------------
// <copyright file="PublishService.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/12/17 14:27:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Al.vNext.Services.Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Al.vNext.Services.Implement
{
    public class PublishService : IPublishService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PublishService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> Send<T>(T data, string queneName)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"rabbitmq://localhost/{queneName}"));
            await endpoint.Send(data);
            return true;
        }
    }
}
