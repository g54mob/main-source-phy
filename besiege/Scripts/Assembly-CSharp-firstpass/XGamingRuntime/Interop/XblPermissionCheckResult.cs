using System;

namespace XGamingRuntime.Interop
{
	internal struct XblPermissionCheckResult
	{
		internal readonly NativeBool isAllowed;

		internal readonly ulong targetXuid;

		internal readonly XblAnonymousUserType targetUserType;

		internal readonly XblPermission permissionRequested;

		private unsafe readonly XblPermissionDenyReasonDetails* reasons;

		internal readonly SizeT reasonsCount;

		internal unsafe T[] GetReasons<T>(Func<XblPermissionDenyReasonDetails, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)reasons, reasonsCount, ctor);
		}
	}
}
