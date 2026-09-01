using System;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerRole
	{
		private unsafe readonly XblMultiplayerRoleType* RoleType;

		internal readonly UTF8StringPtr Name;

		private unsafe readonly ulong* MemberXuids;

		internal readonly uint MemberCount;

		internal readonly uint TargetCount;

		internal readonly uint MaxMemberCount;

		internal unsafe T GetRoleType<T>(Func<XblMultiplayerRoleType, T> ctor) where T : class
		{
			return Converters.PtrToClass((IntPtr)RoleType, ctor);
		}

		internal unsafe T[] GetMemberXuids<T>(Func<ulong, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)MemberXuids, MemberCount, ctor);
		}
	}
}
