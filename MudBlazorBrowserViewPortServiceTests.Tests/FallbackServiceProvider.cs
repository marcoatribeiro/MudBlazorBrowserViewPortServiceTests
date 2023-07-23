using Moq;
using MudBlazor.Services;
using MudBlazor;

namespace MudBlazorBrowserViewPortServiceTests.Tests;

internal sealed class FallbackServiceProvider : IServiceProvider
{
    public FallbackServiceProvider()
    {
        BrowserViewportServiceMock = new();
        DialogServiceMock = new();
        SnackbarMock = CreateSnackbarMock();
    }

    public object? GetService(Type serviceType)
    {
        return serviceType.Name switch
        {
            nameof(IBrowserViewportService) => BrowserViewportServiceMock.Object,
            nameof(IDialogService) => DialogServiceMock.Object,
            nameof(ISnackbar) => SnackbarMock.Object,
            _ => null!
        };
    }

    public Mock<IBrowserViewportService> BrowserViewportServiceMock { get; }
    public Mock<IDialogService> DialogServiceMock { get; }
    public Mock<ISnackbar> SnackbarMock { get; }


    private static Mock<ISnackbar> CreateSnackbarMock()
    {
        var mock = new Mock<ISnackbar>();
        mock.Setup(m => m.Configuration).Returns(new MudServicesConfiguration().SnackbarConfiguration);
        return mock;
    }
}
