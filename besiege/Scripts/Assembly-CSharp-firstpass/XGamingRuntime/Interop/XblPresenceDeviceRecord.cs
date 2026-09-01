using System;

namespace XGamingRuntime.Interop
{
	internal struct XblPresenceDeviceRecord
	{
		internal readonly XblPresenceDeviceType deviceType;

		private readonly IntPtr titleRecords;

		private readonly SizeT titleRecordsCount;

		internal T[] GetTitleRecords<T>(Func<XblPresenceTitleRecord, T> ctor)
		{
			return Converters.PtrToClassArray(titleRecords, titleRecordsCount, ctor);
		}
	}
}
