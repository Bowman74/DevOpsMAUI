
namespace QRTracker.ViewModels;
public class UserInformationViewModel : BaseViewModel
{
    private string _UserName = "";
    public string UserName
    {
        get => _UserName;
        set
        {
            if (_UserName != value)
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }
    }

    private string _Password = "";
    public string Password
    {
        get => _Password;
        set
        {
            if (_Password != value)
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
    }

    private string _BirthDate = "";
    public string BirthDate
    {
        get => _BirthDate;
        set
        {
            if (_BirthDate != value)
            {
                _BirthDate = value;
                OnPropertyChanged();
            }
        }
    }

    private TimeSpan _StartTime = TimeSpan.MinValue;
    public TimeSpan StartTime
    {
        get => _StartTime;
        set
        {
            if (TimeSpan.Compare(_StartTime, value) != 0)
            {
                _StartTime = value;
                OnPropertyChanged();
            }
        }
    }
}
