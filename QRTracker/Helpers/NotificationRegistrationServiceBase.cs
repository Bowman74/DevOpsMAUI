
using Microsoft.Azure.NotificationHubs;
using QRTracker.Interfaces;

namespace QRTracker.Helpers;

public abstract class NotificationRegistrationServiceBase : INotificationRegistrationService
{
    protected readonly IConnectivity _connectivity;
    protected readonly INotificationHubClient _notificationHubClient;
    protected string? _deviceId;

    public NotificationRegistrationServiceBase(IConnectivity connectivity, INotificationHubClient notificationHubClient)
    {
        _connectivity = connectivity;
        _notificationHubClient = notificationHubClient;
    }

    protected abstract Task<Installation?> GetHubInstallation(RegisterDeviceMessage message);

    public async Task<bool> RegisterDeviceWithNotificationHub(RegisterDeviceMessage message)
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return false;
            }

            var installation = await GetHubInstallation(message);
            if (installation == null)
            {
                return false;
            }

            await _notificationHubClient.CreateOrUpdateInstallationAsync(installation);
            return true;

        }
        catch (Exception)
        {
            return false;
        }

    }
}
