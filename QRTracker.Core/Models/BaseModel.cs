﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QRTracker.Shared.Models;
public class BaseModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
