using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;
using System.Fabric;
using System.Threading;
using System.Security.Claims;

namespace Stateless1
{
    public class DelegatedPermissionsRemotingDispatcher : ServiceRemotingDispatcher
    {
        public DelegatedPermissionsRemotingDispatcher(ServiceContext serviceContext, IService service) : base(serviceContext, service)
        {
        }

        public override async Task<byte[]> RequestResponseAsync(IServiceRemotingRequestContext requestContext, ServiceRemotingMessageHeaders messageHeaders, byte[] requestBody)
        {
            byte[] headerValueBytes;
            string headerValueString;

            bool gotCustomHeader = messageHeaders.TryGetHeaderValue("TestHeader", out headerValueBytes);

            if (gotCustomHeader)
            {
                headerValueString = Encoding.UTF8.GetString(headerValueBytes);
                Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("TestHeader", headerValueString) }));
            }

            return await base.RequestResponseAsync(requestContext, messageHeaders, requestBody);
        }
    }
}
