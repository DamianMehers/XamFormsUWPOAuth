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
using System.Threading.Tasks;

namespace XamFormsUWPOAuth.Shared {
  public class Navigator {
    public static Navigator Current { get; } = new Navigator();
    public Func<Task> NavigateToAuthentication { get; set; }
    public Func<Task> AuthenticationComplete { get; set; }
  }
}
