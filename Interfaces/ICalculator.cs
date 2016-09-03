using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICalculator : IService
    {
        Task<int> AddAsync(int x, int y);
        Task<int> SubtractAsync(int x, int y);
    }
}
