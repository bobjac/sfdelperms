using Interfaces;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready to execute?  (Y)es / (N)o?");
            string inputString = Console.ReadLine();

            if (String.Compare(inputString, "Y", true) == 0)
            {
                int[] nums1 = new int[] { 4, 3, 8, 9, 2, 5 };
                int[] nums2 = new int[] { 4, 9, 8, 9, 8, 1 };


                Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("TestClaim1", "TestClaimValue1") }));
                ISumAggregator aggregator = ServiceProxy.Create<ISumAggregator>(new Uri("fabric:/SFDelPerms/Stateless2"));

                Task<int> returnedTask = aggregator.SumOfAggregatesAsync(nums1, nums2);
                
                int x = 0;

                returnedTask.Wait();
                x = returnedTask.Result;


                Console.WriteLine("The result of the call is " + x);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Shutting down");
            }
            
        }
    }
}
