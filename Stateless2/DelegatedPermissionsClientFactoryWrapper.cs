using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using System.Fabric;
using System.Threading;

namespace Stateless2
{
    public class ServiceRemotingClientFactoryWrapper : IServiceRemotingClientFactory
    {
        private readonly IServiceRemotingClientFactory _inner;

        public event EventHandler<CommunicationClientEventArgs<IServiceRemotingClient>> ClientConnected;
        public event EventHandler<CommunicationClientEventArgs<IServiceRemotingClient>> ClientDisconnected;

        public ServiceRemotingClientFactoryWrapper(IServiceRemotingClientFactory inner)
        {
            this._inner = inner;
            this._inner.ClientConnected += _inner_ClientConnected;
            this._inner.ClientDisconnected += _inner_ClientDisconnected;
        }

        private void _inner_ClientDisconnected(object sender, CommunicationClientEventArgs<IServiceRemotingClient> e)
        {
            if (ClientDisconnected != null)
            {
                ClientDisconnected(sender, e);
            }
        }

        private void _inner_ClientConnected(object sender, CommunicationClientEventArgs<IServiceRemotingClient> e)
        {
            if (ClientConnected != null)
            {
                ClientConnected(sender, e);
            }
        }

        private void ServiceRemotingClientFactoryWrapper_ClientConnected(object sender, CommunicationClientEventArgs<IServiceRemotingClient> e)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceRemotingClient> GetClientAsync(ResolvedServicePartition previousRsp, TargetReplicaSelector targetReplicaSelector, string listenerName, OperationRetrySettings retrySettings, CancellationToken cancellationToken)
        {
            var client = await _inner.GetClientAsync(previousRsp, targetReplicaSelector, listenerName, retrySettings, cancellationToken);
            return new ServiceRemotingClientWrapper(client);
        }

        public async Task<IServiceRemotingClient> GetClientAsync(Uri serviceUri, ServicePartitionKey partitionKey, TargetReplicaSelector targetReplicaSelector, string listenerName, OperationRetrySettings retrySettings, CancellationToken cancellationToken)
        {
            var client = await _inner.GetClientAsync(serviceUri, partitionKey, targetReplicaSelector, listenerName, retrySettings, cancellationToken);
            return new ServiceRemotingClientWrapper(client);
        }

        public async Task<OperationRetryControl> ReportOperationExceptionAsync(IServiceRemotingClient client, ExceptionInformation exceptionInformation, OperationRetrySettings retrySettings, CancellationToken cancellationToken)
        {
            return await _inner.ReportOperationExceptionAsync(client, exceptionInformation, retrySettings, cancellationToken);
        }
    }
}
