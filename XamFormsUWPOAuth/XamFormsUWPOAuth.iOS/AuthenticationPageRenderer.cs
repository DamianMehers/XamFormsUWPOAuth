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
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamFormsUWPOAuth;
using XamFormsUWPOAuth.iOS;
using XamFormsUWPOAuth.Shared;

[assembly: ExportRenderer(typeof(AuthenticationPage), typeof(AuthenticationPageRenderer))]

namespace XamFormsUWPOAuth.iOS {
  class AuthenticationPageRenderer : PageRenderer {
    private bool _isShown;

    public override void ViewDidAppear(bool animated) {
      base.ViewDidAppear(animated);

      if (_isShown) return;
      _isShown = true;

      // Initialize the object that communicates with the OAuth service
      var auth = new OAuth2Authenticator(
        Constants.GoogleClientId,
        Constants.GoogleClientSecret,
        Constants.Scope,
        new Uri(Constants.AuthorizeUrl),
        new Uri(Constants.GoogleCallbackUrl),
        new Uri(Constants.AccessTokenUrl));

      // Register an event handler for when the authentication process completes
      auth.Completed += OnAuthenticationCompleted;

      // Display the UI
      PresentViewController(auth.GetUI(), true, null);
    }

    async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e) {
      if (e.IsAuthenticated) {
        await AuthenticationHelper.FetchGoogleEmailAndPicture(e.Account);
      }
    }
  }
}
