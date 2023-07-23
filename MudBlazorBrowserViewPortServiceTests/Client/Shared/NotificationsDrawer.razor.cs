using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MudBlazorBrowserViewPortServiceTests.Client.Shared;

public partial class NotificationsDrawer
{
    [Parameter]
    public bool Open
    {
        get => _open;
        set
        {
            switch (value)
            {
                case true when value != _open:
                    _shouldLoadNotifications = true;
                    break;
                case false when value != _open:
                    InvokeAsync(SetNotificationsAsReadAsync);
                    break;
            }

            _open = value;
        }
    }
    private bool _open;

    [Parameter] public EventCallback<bool> OpenChanged { get; set; }

    private ErrorBoundary? _errorBoundary;
    private bool _shouldLoadNotifications;

    protected override void OnAfterRender(bool firstRender)
    {
        if (_shouldLoadNotifications)
            _shouldLoadNotifications = false;
    }

    private void ReloadNotificationsList()
    {
        _shouldLoadNotifications = true;
        _errorBoundary?.Recover();
    }

    private async Task SetNotificationsAsReadAsync()
    {
        // Omitted code...
    }

    private async Task CloseDrawerAsync() => await OpenChanged.InvokeAsync(false);
}
