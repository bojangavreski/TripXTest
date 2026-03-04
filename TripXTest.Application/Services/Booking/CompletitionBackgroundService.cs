using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using TripXTest.Application.Contracts;

namespace TripXTest.Application.Services
{
    public class CompletitionBackgroundService : BackgroundService
    {
        private readonly Channel<(string Code, int Delay)> _queue = Channel.CreateUnbounded<(string, int)>();
        private readonly IServiceScopeFactory _scopeFactory;

        public CompletitionBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void TriggerWithInterval(int seconds, string bookingCode)
        {
            _queue.Writer.TryWrite((bookingCode, seconds));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var (code, delay) in _queue.Reader.ReadAllAsync())
            {
                _ = ChangeBookingStatus(code, delay);
            }
        }

        private async Task ChangeBookingStatus(string bookingCode, int sleepSeconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(sleepSeconds));

            await using var scope = _scopeFactory.CreateAsyncScope();  
            var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
            bookingService.CompleteBooking(bookingCode);
        }
    }
}
