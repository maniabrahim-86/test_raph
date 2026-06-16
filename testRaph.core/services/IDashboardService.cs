using System.Collections.Generic;
using System.Threading.Tasks;

namespace testRaph.core.services
{
    public interface IDashboardService
    {
        Task StartAsync(CancellationToken cancellationToken);
        // Task RefreshLoopAsync(CancellationToken cancellationToken);
        // Task PollingLoopAsync(CancellationToken cancellationToken);
    }
    public class DashboardService : IDashboardService
    {
        private readonly IMessengerService _message;
        public DashboardService(IMessengerService message)
        {
            _message = message;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _message.DataRefreshed += Refresh2;

           try
           {
             var refreshTask = RefreshLoopAsync(cancellationToken);
             var pollingTask = PollingLoopAsync(cancellationToken);
 
             await Task.WhenAll(refreshTask, pollingTask);
           }
           catch (OperationCanceledException)
           {
                Console.WriteLine("Annulation demandée");
           }
           finally
            {
                _message.DataRefreshed -= Refresh2;  
            }
        }
        private async Task RefreshLoopAsync(CancellationToken cancellationToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                Refresh();
            }
        }

        private async Task PollingLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
                await PollApiAsync();
            }
        }
        private void Refresh()
        {
            Console.WriteLine($"Nouveau message : {DateTime.Now:T}");
        }
        private void Refresh2()
        {
            Console.WriteLine($"Nouveau Refresh2 message : {DateTime.Now:T}");
        }
        private Task PollApiAsync()
        {
            Console.WriteLine($"Polling : {DateTime.Now:T}");
            return Task.CompletedTask;
        }

    }

}
