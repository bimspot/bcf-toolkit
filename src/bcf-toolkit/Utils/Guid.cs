using System;
using System.Security.Cryptography;
using System.Text;

namespace BcfToolkit.Utils;

public static class GuidUtils {
  /// <summary>
  ///   Method generates a new guid based on the input string. It creates an
  ///   MD5 hash from the data, then turns to GUID.
  /// </summary>
  /// <param name="input">Input data on which the hash is generated.</param>
  /// <returns>Returns the GUID based on the input data.</returns>
  public static string NewGuidByContent(string input) {
    var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
    return new Guid(hash).ToString();
  }
}