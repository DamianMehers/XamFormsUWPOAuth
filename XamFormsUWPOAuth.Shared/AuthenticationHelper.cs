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

using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Auth;
namespace XamFormsUWPOAuth.Shared {
  public static class AuthenticationHelper {
    public const string AppName = "XamFormsUWPOAuth";

    public static async Task FetchGoogleEmailAndPicture(Account account) {
      var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, account);
      var response = await request.GetResponseAsync();
      if (response != null) {
        var userJson = response.GetResponseText();
        var user = JsonConvert.DeserializeObject<User>(userJson);
        account.Username = user.Email;
        account.Properties[Constants.EmailAccountProperty] = user.Email;
        account.Properties[Constants.PhotoAccountProperty] = user.Picture;
        AccountStoreFactory.Create().Save(account, AppName);
        UpdateViewModel(account);
      }

      await Navigator.Current.AuthenticationComplete();
    }

    public static async Task<bool> IsAlreadyAuthenticated() {
      // Retrieve any stored account information
      var accounts = await AccountStoreFactory.Create().FindAccountsForServiceAsync(AppName);
      var account = accounts.FirstOrDefault();

      // If we already have the account info then we are set
      if (account == null) return false;
      UpdateViewModel(account);
      return true;
    }

    private static void UpdateViewModel(Account account) {
      Locator.MainViewModel.EmailAddress = account.Properties[Constants.EmailAccountProperty];
      Locator.MainViewModel.PhotoUrl = account.Properties[Constants.PhotoAccountProperty];
    }
  }
}
