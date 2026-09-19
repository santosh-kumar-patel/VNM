using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.Helper
{
    public static class HttpResiliencePolicyProvider
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (result, breakDelay) =>
                        Console.WriteLine($"Circuit broken: {result.Exception?.Message}"),
                    onReset: () =>
                        Console.WriteLine("Circuit reset"),
                    onHalfOpen: () =>
                        Console.WriteLine("Circuit half-open")
                );
        }

    }
}
