using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	internal static class OidLookup
	{
		private static readonly ConcurrentDictionary<string, string> s_lateBoundOidToFriendlyName;

		private static readonly ConcurrentDictionary<string, string> s_lateBoundFriendlyNameToOid;

		private static readonly Dictionary<string, string> s_friendlyNameToOid;

		private static readonly Dictionary<string, string> s_oidToFriendlyName;

		private static readonly Dictionary<string, string> s_compatOids;

		public static string ToFriendlyName(string oid, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return null;
		}

		public static string ToOid(string friendlyName, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return null;
		}

		private static bool ShouldUseCache(OidGroup oidGroup)
		{
			return false;
		}

		private static string NativeOidToFriendlyName(string oid, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return null;
		}

		private static string NativeFriendlyNameToOid(string friendlyName, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return null;
		}
	}
}
