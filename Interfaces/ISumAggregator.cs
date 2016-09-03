using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISumAggregator : IService
    {
        Task<int> SumOfAggregatesAsync(int[] array1, int[] array2);
    }

   // public class CustomPrincipal
}
