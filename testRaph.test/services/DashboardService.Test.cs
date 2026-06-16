using System.Threading;
using System.Threading.Tasks;
using Xunit;

using testRaph.core.services;

namespace testRaph.Tests;

public class DashboardServiceTests
{
    [Fact]
    public async Task StartAsync_Should_Not_Throw_When_Cancellation()
    {
        // Arrange
        var messenger = new MessengerService();
        var dashboard = new DashboardService(messenger);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        var exception = await Record.ExceptionAsync(
            () => dashboard.StartAsync(cts.Token));

        // Assert
        Assert.Null(exception);
    }
}