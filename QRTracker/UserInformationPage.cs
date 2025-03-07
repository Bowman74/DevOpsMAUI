using QRTracker.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace QRTracker;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class UserInformationPage : ContentPage
{
    private bool _timeSelectedChanging = false;

    [RequiresUnreferencedCode("Calls LoadFromXML.")]
    public UserInformationPage(UserInformationViewModel userInformationViewModel)
	{
        var stream = FileSystem.OpenAppPackageFileAsync("UserInformationPage.xml");
        var reader = new StreamReader(stream.Result);
        var pagexaml = reader.ReadToEnd();

        this.LoadFromXaml(pagexaml);
        this.BindingContext = userInformationViewModel;
    }

    private void TimePicker_TimeSelected(object sender, TimeChangedEventArgs e)
    {
        if (_timeSelectedChanging)
        {
            _timeSelectedChanging = false;
            return;
        }

        var timeDifference = e.NewTime - e.OldTime;

        if (Math.Abs(timeDifference.TotalMinutes) > 120)
        {
            if (this.BindingContext != null)
            {
                _timeSelectedChanging = true;
                var viewModel = (UserInformationViewModel)this.BindingContext;
                viewModel.StartTime = e.OldTime;
            }
        }
    }
}