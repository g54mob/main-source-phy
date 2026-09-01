using System;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerRoleType
	{
		internal readonly UTF8StringPtr Name;

		internal readonly NativeBool OwnerManaged;

		internal readonly XblMutableRoleSettings MutableRoleSettings;

		private unsafe readonly XblMultiplayerRole* Roles;

		internal readonly SizeT RoleCount;

		internal unsafe T[] GetRoles<T>(Func<XblMultiplayerRole, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)Roles, RoleCount, ctor);
		}
	}
}
