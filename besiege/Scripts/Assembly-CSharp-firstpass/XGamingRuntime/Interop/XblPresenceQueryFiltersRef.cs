using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	internal class XblPresenceQueryFiltersRef
	{
		private readonly IntPtr deviceTypes;

		private readonly SizeT deviceTypesCount;

		private readonly IntPtr titleIds;

		private readonly SizeT titleIdsCount;

		internal readonly XblPresenceDetailLevel detailLevel;

		[MarshalAs(UnmanagedType.U1)]
		internal bool onlineOnly;

		[MarshalAs(UnmanagedType.U1)]
		internal bool broadcastingOnly;

		internal XblPresenceQueryFiltersRef(XblPresenceQueryFilters filters, DisposableCollection disposableCollection)
		{
			deviceTypes = Converters.ClassArrayToPtr(filters.DeviceTypes, (Func<XblPresenceDeviceType, DisposableCollection, XblPresenceDeviceType>)((XblPresenceDeviceType dt, DisposableCollection _) => dt), disposableCollection, out deviceTypesCount);
			titleIds = Converters.ClassArrayToPtr(filters.TitleIds, (Func<uint, DisposableCollection, uint>)((uint titleId, DisposableCollection _) => titleId), disposableCollection, out titleIdsCount);
			detailLevel = filters.DetailLevel;
			onlineOnly = filters.OnlineOnly;
			broadcastingOnly = filters.BroadcastingOnly;
		}
	}
}
