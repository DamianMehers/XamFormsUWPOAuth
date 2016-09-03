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

// Inspired by https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/WebAuthenticationBroker

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Forms.Platform.UWP;
using XamFormsUWPOAuth;
using XamFormsUWPOAuth.Shared;
using XamFormsUWPOAuth.UWP;

[assembly: ExportRenderer(typeof(AuthenticationPage), typeof(AuthenticationPageRenderer))]

namespace XamFormsUWPOAuth.UWP {
  class AuthenticationPageRenderer : PageRenderer {
    private bool _isShown;

    protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
      base.OnElementPropertyChanged(sender, e);
      if (_isShown) return;
      _isShown = true;

      var code = await AuthenticateUsingWebAuthenticationBroker();
      var account = await ConvertCodeToAccount(code);
      await AuthenticationHelper.FetchGoogleEmailAndPicture(account);
    }


    private async Task<string> AuthenticateUsingWebAuthenticationBroker() {
      var googleUrl = Constants.AuthorizeUrl + "?client_id=" +
                      Uri.EscapeDataString(Constants.GoogleClientId);
      googleUrl += "&redirect_uri=" + Uri.EscapeDataString(Constants.GoogleCallbackUrl);
      googleUrl += "&response_type=code";
      googleUrl += "&scope=" + Uri.EscapeDataString(Constants.Scope);

      var startUri = new Uri(googleUrl);

      var webAuthenticationResult =
        await
          WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri,
            new Uri(Constants.GoogleCallbackUrl));
      return webAuthenticationResult.ResponseStatus != WebAuthenticationStatus.Success ? null : webAuthenticationResult.ResponseData.Substring(webAuthenticationResult.ResponseData.IndexOf('=') + 1);
    }


    private static async Task<Account> ConvertCodeToAccount(string code) {
      var httpClient = new HttpClient();
      IHttpContent content = new HttpFormUrlEncodedContent(new Dictionary<string, string> {
        {"code", code},
        {"client_id", Constants.GoogleClientId},
        {"client_secret", Constants.GoogleClientSecret},
        {"redirect_uri", Constants.GoogleCallbackUrl},
        {"grant_type", "authorization_code"},
      });
      var accessTokenResponse = await httpClient.PostAsync(new Uri(Constants.AccessTokenUrl), content);
      var responseDict =
        JsonConvert.DeserializeObject<Dictionary<string, string>>(accessTokenResponse.Content.ToString());

      return new Account(null, responseDict);
    }
  }
}

