using System;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionInitArgs
	{
		internal readonly uint MaxMembersInSession;

		internal readonly XblMultiplayerSessionVisibility Visibility;

		private unsafe readonly ulong* InitiatorXuids;

		internal readonly SizeT InitiatorXuidsCount;

		internal readonly UTF8StringPtr CustomJson;

		internal unsafe XblMultiplayerSessionInitArgs(XGamingRuntime.XblMultiplayerSessionInitArgs publicObject, DisposableCollection disposableCollection)
		{
			MaxMembersInSession = publicObject.MaxMembersInSession;
			Visibility = publicObject.Visibility;
			InitiatorXuids = (ulong*)(void*)Converters.ClassArrayToPtr(publicObject.InitiatorXuids, (Func<ulong, DisposableCollection, ulong>)((ulong x, DisposableCollection _) => x), disposableCollection, out InitiatorXuidsCount);
			CustomJson = new UTF8StringPtr(publicObject.CustomJson, disposableCollection);
		}

		internal unsafe T[] GetInitiatorXuids<T>(Func<ulong, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)InitiatorXuids, InitiatorXuidsCount, ctor);
		}
	}
}
