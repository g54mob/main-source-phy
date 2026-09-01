using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblPresenceTitleRecord
	{
		internal readonly uint titleId;

		internal readonly UTF8StringPtr titleName;

		internal readonly TimeT lastModified;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool titleActive;

		internal readonly UTF8StringPtr richPresenceString;

		internal readonly XblPresenceTitleViewState viewState;

		private readonly IntPtr broadcastRecord;

		internal T GetBroadcastRecord<T>(Func<XblPresenceBroadcastRecord, T> ctor) where T : class
		{
			return Converters.PtrToClass(broadcastRecord, ctor);
		}
	}
}
