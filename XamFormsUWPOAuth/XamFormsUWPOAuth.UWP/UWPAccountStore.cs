// Based on https://github.com/xamarin/Xamarin.Auth/blob/portable-bait-and-switch/source/Xamarin.Auth.UniversalWindowsPlatform/PlatformSpecific/UWPAccountStore.Async.cs
using System.Collections.Generic;
using Xamarin.Auth;

namespace XamFormsUWPOAuth.UWP {
  internal partial class UWPAccountStore : AccountStore {
    public override IEnumerable<Account> FindAccountsForService(string serviceId) {
      return FindAccountsForServiceAsync(serviceId).Result;
    }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    public override void Save(Account account, string serviceId) {
      SaveAsync(account, serviceId);
    }

    public override void Delete(Account account, string serviceId) {
      DeleteAsync(account, serviceId);
    }
#pragma warning restore 4014
  }
}
