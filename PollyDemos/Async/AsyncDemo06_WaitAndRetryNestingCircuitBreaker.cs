using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.CircuitBreaker;
using PollyDemos.OutputHelpers;

namespace PollyDemos.Async
{
    public class AsyncDemo06_WaitAndRetryNestingCircuitBreaker : AsyncDemo
    {

        //private int totalRequests;

        //private int eventualSuccesses;

        //private int retries;

        //private int eventualFailuresDueToCircuitBreaking;

        //private int eventualFailuresForOtherReasons;

        //public new string Description =
        //    "This demonstrates CircuitBreaker. When an underlying system is completely down or seriously struggling, it can be better to fail fast and not put calls through.";


        //public AsyncDemo06_WaitAndRetryNestingCircuitBreaker(IConfiguration configuration) : base(configuration)
        //{
        //}

        //public async override Task ExecuteAsync(CancellationToken cancellationToken, IProgress<DemoProgress> progress)
        //{
        //    if (cancellationToken == null) throw new ArgumentNullException(nameof(cancellationToken));
        //    if (progress == null) throw new ArgumentNullException(nameof(progress));


        //    eventualSuccesses = 0;
        //    retries = 0;
        //    eventualFailuresDueToCircuitBreaking = 0;
        //    eventualFailuresForOtherReasons = 0;

        //    progress.Report(ProgressWithMessage(typeof(AsyncDemo06_WaitAndRetryNestingCircuitBreaker).Name));
        //    progress.Report(ProgressWithMessage("===="));
        //    progress.Report(ProgressWithMessage(String.Empty));

        //    var waitAndRetryPolicy = Policy.Handle<Exception>(e => !(e is BrokenCircuitException))
        //        .WaitAndRetryForeverAsync(
        //            attempt => TimeSpan.FromMilliseconds(200),
        //            (exception, calculatedWaitDuration) =>
        //            {
        //                progress.Report(ProgressWithMessage(
        //                    ".Log,then retry:" + exception.Message, Color.Yellow));
        //                retries++;
        //            }
        //        );

        //    var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(
        //        exceptionsAllowedBeforeBreaking: 4,
        //        durationOfBreak: TimeSpan.FromSeconds(3),
        //        onBreak: (ex, breakDelay) =>
        //        {
        //            progress.Report(ProgressWithMessage(
        //                ".Breaker logging: Breaking the circuit for " + breakDelay.TotalMilliseconds + "ms!",
        //                Color.Magenta));
        //            progress.Report(ProgressWithMessage("..due to:" + ex.Message, Color.Magenta));
        //        },
        //        onReset: () =>
        //            progress.Report(ProgressWithMessage(".Breaker logging: Call ok! Closed the circuit again!",
        //                Color.Magenta)),
        //        onHalfOpen: () =>
        //            progress.Report(ProgressWithMessage(".Breaker logging: Half-open: Next call is a trial!",
        //                Color.Magenta))
        //    );

        //    using (var client = new HttpClient())
        //    {
        //        totalRequests = 0;
        //        bool internalCancel = false;

        //        while (!internalCancel && !cancellationToken.IsCancellationRequested)
        //        {
        //            totalRequests++;
        //            Stopwatch watch = new Stopwatch();
        //            watch.Start();
        //            try
        //            {
        //                await waitAndRetryPolicy.ExecuteAsync(async token =>
        //                {
        //                    string response = await circuitBreakerPolicy.ExecuteAsync<String>(() =>
        //                        client.GetStringAsync(
        //                            Configuration["WEB_API_ROOT"] + "/api/values/" + totalRequests));

        //                    watch.Stop();

        //                    progress.Report(ProgressWithMessage(
        //                        "Response: " + response + " (after " + watch.ElapsedMilliseconds + "ms)", Color.Green));

        //                    eventualSuccesses++;
        //                }, cancellationToken);
        //            }
        //            catch (BrokenCircuitException b)
        //            {
        //                watch.Stop();

        //                progress.Report(ProgressWithMessage(
        //                    "Request " + totalRequests + " failed with:" + b.GetType().Name + " (after " +
        //                    watch.ElapsedMilliseconds + "ms)", Color.Red));

        //                eventualFailuresDueToCircuitBreaking++;
        //            }
        //            catch (Exception e)
        //            {
        //                watch.Stop();
                        
        //                progress.Report(ProgressWithMessage("Request " + totalRequests + " eventually failed with: " + e.Message + " （after" + watch.ElapsedMilliseconds + "ms)", Color.Red));

        //                eventualFailuresForOtherReasons++;
        //            }

        //            await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);

        //            internalCancel = TerminateDemosByKeyPress && Console.KeyAvailable;
        //        }
        //    }

        //}

        //public override Statistic[] LasteStatistics => new [] {
        //    new Statistic("Total requests made", totalRequests),
        //    new Statistic("Requests which eventually successded", eventualSuccesses, Color.Green),
        //    new Statistic("Retries made to help achieve success", retries, Color.Yellow),
        //    new Statistic("Requests failed early by broken circuit", eventualFailuresDueToCircuitBreaking, Color.Magenta),
        //    new Statistic("Requests which failed after longer delay", eventualFailuresForOtherReasons, Color.Red),
        //};
    }
}
