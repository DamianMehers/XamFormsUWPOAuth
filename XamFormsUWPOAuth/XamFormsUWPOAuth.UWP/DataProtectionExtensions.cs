// From https://github.com/igorkulman/Kulman.UWP/blob/master/Kulman.UWP/Code/DataProtectionExtensions.cs

using System;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace XamFormsUWPOAuth.UWP {
  public static class DataProtectionExtensions {
    /// <summary>
    /// Encrypts given text in given scope
    /// </summary>
    /// <param name="clearText">Clear text to encrypt</param>
    /// <param name="scope">Scope</param>
    /// <returns>Encrypted text</returns>
    public static async Task<string> ProtectAsync(string clearText, string scope = "LOCAL=user") {
      if (clearText == null) {
        throw new ArgumentNullException(nameof(clearText));
      }
      if (scope == null) {
        throw new ArgumentNullException(nameof(scope));
      }

      var clearBuffer = CryptographicBuffer.ConvertStringToBinary(clearText, BinaryStringEncoding.Utf8);
      var provider = new DataProtectionProvider(scope);
      var encryptedBuffer = await provider.ProtectAsync(clearBuffer);
      return CryptographicBuffer.EncodeToBase64String(encryptedBuffer);
    }

    public static async Task<IBuffer> ProtectAsync(byte[] clearText, string scope = "LOCAL=user") {
      if (clearText == null) {
        throw new ArgumentNullException(nameof(clearText));
      }
      if (scope == null) {
        throw new ArgumentNullException(nameof(scope));
      }

      var clearBuffer = CryptographicBuffer.CreateFromByteArray(clearText);
      var provider = new DataProtectionProvider(scope);
      var encryptedBuffer = await provider.ProtectAsync(clearBuffer);
      return encryptedBuffer;
      //return CryptographicBuffer.EncodeToBase64String(encryptedBuffer);
    }


    /// <summary>
    /// Decrypts given text
    /// </summary>
    /// <param name="encryptedText">Encrypted text</param>
    /// <returns>Clear text</returns>

    public static async Task<string> UnprotectAsync(string encryptedText) {
      if (encryptedText == null) {
        throw new ArgumentNullException(nameof(encryptedText));
      }

      var encryptedBuffer = CryptographicBuffer.DecodeFromBase64String(encryptedText);
      var provider = new DataProtectionProvider();
      var clearBuffer = await provider.UnprotectAsync(encryptedBuffer);
      return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, clearBuffer);
    }

    public static async Task<IBuffer> UnprotectAsync(byte[] encryptedText) {
      if (encryptedText == null) {
        throw new ArgumentNullException(nameof(encryptedText));
      }

      var encryptedBuffer = CryptographicBuffer.CreateFromByteArray(encryptedText);
      var provider = new DataProtectionProvider();
      var clearBuffer = await provider.UnprotectAsync(encryptedBuffer);
      return clearBuffer;
      //return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, clearBuffer);
    }

  }

}
