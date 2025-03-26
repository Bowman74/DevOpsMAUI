
using Microsoft.VisualBasic;
using NewRelic.MAUI.Plugin;
using QRTracker.Constants;
using QRTracker.Shared.Models;

namespace QRTracker.ViewModels;
public class QRItemDetailViewModel : BaseViewModel
{
    private QRCodeItem? _QRCodeItem = null;
    public QRCodeItem? QRCodeItem
    {
        get => _QRCodeItem;
        set
        {
            if (_QRCodeItem != value)
            {
                _QRCodeItem = value;
                OnPropertyChanged();
            }
        }
    }

    private Command? _SaveCommand;

    public Command SaveCommand => _SaveCommand ??= new Command(async () => await OnSave());

    private async Task OnSave()
    {
#if ANDROID || IOS
        string interactionId = string.Empty;
        try
        {
            interactionId = CrossNewRelic.Current.StartInteraction("QR Item Updated");
#endif
            var pageParams = new ShellNavigationQueryParameters();
            if (QRCodeItem != null)
            {
                pageParams.Add("UpdatedQRCode", QRCodeItem);
#if ANDROID || IOS
                var success = CrossNewRelic.Current.RecordCustomEvent("Database Operations", "QR Item Updated", new Dictionary<string, object>
                {
                    { "Interaction Id", interactionId},
                    { "Item Id", QRCodeItem.Id }
                });
#endif
            }

            await Shell.Current.GoToAsync("..", true, pageParams);
#if ANDROID || IOS
        }
        catch (Exception ex)
        {
            CrossNewRelic.Current.RecordException(ex);
            throw;
        }
        finally
        {
            CrossNewRelic.Current.EndInteraction(interactionId);
        }
#endif
    }

    private Command? _CancelCommand;

    public Command CancelCommand => _CancelCommand ??= new Command(async () => await OnCancel());

    private async Task OnCancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
