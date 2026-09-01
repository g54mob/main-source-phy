using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	internal class XblPresenceRichPresenceIdsRef
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		internal readonly byte[] scid;

		internal readonly UTF8StringPtr presenceId;

		private readonly IntPtr presenceTokenIds;

		private readonly SizeT presenceTokenIdsCount;

		internal XblPresenceRichPresenceIdsRef(XblPresenceRichPresenceIds richPresenceIds, DisposableCollection disposableCollection)
		{
			scid = Converters.StringToNullTerminatedUTF8ByteArray(richPresenceIds.ServiceConfigurationId, 40);
			presenceId = new UTF8StringPtr(richPresenceIds.PresenceId, disposableCollection);
			presenceTokenIds = Converters.StringArrayToUTF8StringArray(richPresenceIds.PresenceTokenIds, disposableCollection, out presenceTokenIdsCount);
		}

		internal static bool ValidateFields(string scid)
		{
			return scid != null && Converters.StringToNullTerminatedUTF8ByteArray(scid).Length <= 40;
		}
	}
}
