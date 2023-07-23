using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Services;

namespace MudBlazorBrowserViewPortServiceTests.Tests;

internal static class TestContextExtensions
{
    // Source: https://github.com/MudBlazor/MudBlazor/blob/dev/src/MudBlazor/Extensions/ServiceCollectionExtensions.cs
    public static void AddTestServices(this TestContext ctx)
    {
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        ctx.Services
#pragma warning disable CS0618
            // If the 2 lines below are commented (and the one below them is not) the test does not work.
            //.AddScoped<IBrowserWindowSizeProvider, BrowserWindowSizeProvider>()
            //.AddScoped<IBreakpointService, BreakpointService>()
#pragma warning restore CS0618
            .AddMudBlazorResizeListener() // If this line is commented (and the 2 above are not) the test works.
            .AddMudBlazorResizeObserver()
            .AddMudBlazorResizeObserverFactory()
            .AddMudBlazorKeyInterceptor()
            .AddMudBlazorJsEvent()
            .AddMudBlazorScrollManager()
            .AddMudBlazorScrollListener()
            .AddMudBlazorJsApi()
            .AddMudBlazorScrollSpy()
            .AddMudPopoverService()
            .AddMudEventManager();

        ctx.Services.AddScoped(_ => new HttpClient());
        ctx.Services.AddOptions();
    }

    public static void AddTestServicesAndFallbackServiceProvider(this TestContext ctx, FallbackServiceProvider? serviceProvider = null)
    {
        ctx.AddTestServices();
        ctx.Services.AddFallbackServiceProvider(serviceProvider ?? new FallbackServiceProvider());
    }

    public static void AddJSInProcessRuntime(this TestContext ctx)
    {
        ctx.Services.AddSingleton(services => (IJSInProcessRuntime)services.GetRequiredService<IJSRuntime>());
    }
}
