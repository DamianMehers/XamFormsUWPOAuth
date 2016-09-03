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

namespace XamFormsUWPOAuth.Shared {
public static class AccountStoreFactory {
    public static Func<AccountStore> Create { get; set; } = () => AccountStore.Create();
  }
}

