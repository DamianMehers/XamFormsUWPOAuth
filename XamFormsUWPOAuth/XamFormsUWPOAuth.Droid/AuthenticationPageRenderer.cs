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
using Android.App;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamFormsUWPOAuth;
using XamFormsUWPOAuth.Droid;
using XamFormsUWPOAuth.Shared;

[assembly: ExportRenderer(typeof(AuthenticationPage), typeof(AuthenticationPageRenderer))]
namespace XamFormsUWPOAuth.Droid {
  class AuthenticationPageRenderer : PageRenderer{
    private bool _isShown;

    protected override void OnElementChanged(ElementChangedEventArgs<Page> e) {
      base.OnElementChanged(e);

      if (_isShown) return;
      _isShown = true;

      var auth = new OAuth2Authenticator(
        Constants.GoogleClientId,
        Constants.GoogleClientSecret,
        Constants.Scope,
        new Uri(Constants.AuthorizeUrl),
        new Uri(Constants.GoogleCallbackUrl),
        new Uri(Constants.AccessTokenUrl));
      auth.Completed += OnAuthenticationCompleted;

      // Display the UI
      var activity = Context as Activity;
      activity?.StartActivity(auth.GetUI(activity));
    }

    private async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e) {
      if (e.IsAuthenticated) {
        await AuthenticationHelper.FetchGoogleEmailAndPicture(e.Account);
      }
    }
  }
}