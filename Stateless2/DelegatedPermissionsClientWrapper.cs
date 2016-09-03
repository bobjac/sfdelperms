using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using System.Fabric;

namespace Stateless2
{
    public class ServiceRemotingClientWrapper : IServiceRemotingClient
    {
        private readonly IServiceRemotingClient _inner;

        public ServiceRemotingClientWrapper(IServiceRemotingClient inner)
        {
            this._inner = inner;
        }

        public ResolvedServiceEndpoint Endpoint
        {
            get
            {
                return _inner.Endpoint;
            }

            set
            {
                _inner.Endpoint = value;
            }
        }

        public string ListenerName
        {
            get
            {
                return _inner.ListenerName;
            }

            set
            {
                _inner.ListenerName = value;
            }
        }

        public ResolvedServicePartition ResolvedServicePartition
        {
            get
            {
                return _inner.ResolvedServicePartition;
            }

            set
            {
                _inner.ResolvedServicePartition = value;
            }
        }

        public Task<byte[]> RequestResponseAsync(ServiceRemotingMessageHeaders messageHeaders, byte[] requestBody)
        {
            // add custom header
            messageHeaders.AddHeader("TestHeader", Encoding.UTF8.GetBytes("TestHeaderValue"));

            return _inner.RequestResponseAsync(messageHeaders, requestBody);
        }

        public void SendOneWay(ServiceRemotingMessageHeaders messageHeaders, byte[] requestBody)
        {
            // add custom header
            messageHeaders.AddHeader("TestHeader", Encoding.UTF8.GetBytes("TestHeaderValue"));

            _inner.SendOneWay(messageHeaders, requestBody);
        }
    }
}
