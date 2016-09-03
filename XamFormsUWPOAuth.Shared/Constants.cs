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
namespace XamFormsUWPOAuth.Shared {
  public static class Constants {

    // Fill in these values from an app you create at https://console.developers.google.com using
    // a web OAuth Client
    public const string GoogleCallbackUrl = null;
    public const string GoogleClientId = null;
    public const string GoogleClientSecret = null;

    public const string Scope = "email";
    public const string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
    public const string AccessTokenUrl = "https://accounts.google.com/o/oauth2/token";
    public const string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
    public const string EmailAccountProperty = "email";
    public const string PhotoAccountProperty = "photo";
  }
}
