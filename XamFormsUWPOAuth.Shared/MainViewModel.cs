//*********************************************************
//
// Copyright (c) Damian Mehers. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamFormsUWPOAuth.Shared {
  public class MainViewModel : INotifyPropertyChanged {
    private string _emailAddress;
    private string _photoUrl;
    public event PropertyChangedEventHandler PropertyChanged;

    public MainViewModel() {
      LaunchCommand = new Command(Launch);
    }

    public string EmailAddress {
      get { return _emailAddress; }
      set {
        if (_emailAddress == value) return;
        _emailAddress = value;
        OnPropertyChanged();
      }
    }

    public string PhotoUrl {
      get { return _photoUrl; }
      set {
        if (_photoUrl == value) return;
        _photoUrl = value;
        OnPropertyChanged();
      }
    }

    public ICommand LaunchCommand { get; private set; }

    public async void Launch() {
      if(await AuthenticationHelper.IsAlreadyAuthenticated()) return;
      await Navigator.Current.NavigateToAuthentication();
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
