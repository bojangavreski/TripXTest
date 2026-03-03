using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripXTest.Application.Contracts;

namespace TripXTest.Application.Services
{
    public class CompletitionBackgroundService : BackgroundService
    {
        private PeriodicTimer? _timer;
        private int _intervalSeconds = 30; 
        private readonly SemaphoreSlim _restartSignal = new(0, 1);
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Stack<string> _bookingCodes = new Stack<string>();

        public CompletitionBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void TriggerWithInterval(int seconds, string bookingCode)
        {
            _intervalSeconds = seconds;
            _bookingCodes.Push(bookingCode);

            if (_restartSignal.CurrentCount == 0)
                _restartSignal.Release();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _restartSignal.WaitAsync(stoppingToken);

                _timer = new PeriodicTimer(TimeSpan.FromSeconds(_intervalSeconds));

                try
                {
                    while (await _timer.WaitForNextTickAsync(stoppingToken))
                    {
                        if (_restartSignal.CurrentCount > 0 && _bookingCodes.Count == 0)
                        {
                            break; 
                        }
                        await using var scope = _scopeFactory.CreateAsyncScope();
                        var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                        bookingService.CompleteBooking(_bookingCodes.Pop());
                    }
                }
                finally
                {
                    _timer.Dispose();
                }
            }
        }
    }
}
