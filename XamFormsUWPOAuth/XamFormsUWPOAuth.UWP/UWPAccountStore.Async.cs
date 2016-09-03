// Based on https://github.com/xamarin/Xamarin.Auth/blob/portable-bait-and-switch/source/Xamarin.Auth.UniversalWindowsPlatform/PlatformSpecific/UWPAccountStore.Async.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Auth;

namespace XamFormsUWPOAuth.UWP
{
  internal partial class UWPAccountStore {
    public override async Task<List<Account>> FindAccountsForServiceAsync(string serviceId) {
      var localFolder = ApplicationData.Current.LocalFolder;

      var files = await localFolder.GetFilesAsync().AsTask().ConfigureAwait(false);

      var accounts = new List<Account>();

      foreach (var file in files.Where(x => x.Name.StartsWith("xamarin.auth.") &&
                                            x.Name.EndsWith("." + serviceId))
                                .ToList()) {
        using (var stream = await file.OpenStreamForReadAsync().ConfigureAwait(false))
        using (var reader = new BinaryReader(stream)) {
          var length = reader.ReadInt32();
          var data = reader.ReadBytes(length);

          var unprot = (await DataProtectionExtensions.UnprotectAsync(data).ConfigureAwait(false)).ToArray();
          accounts.Add(Account.Deserialize(Encoding.UTF8.GetString(unprot, 0, unprot.Length)));
        }
      }

      return accounts;
    }

    public override async Task DeleteAsync(Account account, string serviceId) {
      var path = GetAccountPath(account, serviceId);
      try {
        var localFolder = ApplicationData.Current.LocalFolder;
        var file = await localFolder.GetFileAsync(path).AsTask().ConfigureAwait(false);
        await file.DeleteAsync().AsTask().ConfigureAwait(false);
      } catch {
        // Ignore this error if file doesn't exist
      }
    }

    public override async Task SaveAsync(Account account, string serviceId) {
      byte[] data = Encoding.UTF8.GetBytes(account.Serialize());
      byte[] prot = (await DataProtectionExtensions.ProtectAsync(data).ConfigureAwait(false)).ToArray();

      var path = GetAccountPath(account, serviceId);

      var localFolder = ApplicationData.Current.LocalFolder;
      var file = await localFolder.CreateFileAsync(path, CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);
      using (var stream = await file.OpenStreamForWriteAsync().ConfigureAwait(false))
      using (var writer = new BinaryWriter(stream)) {
        writer.Write(prot.Length);
        writer.Write(prot);
      }
    }

    private static string GetAccountPath(Account account, string serviceId) {
      return string.Format("xamarin.auth.{0}.{1}", account.Username, serviceId);
    }
  }

}
