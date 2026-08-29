using System;
using System.Reflection;

namespace Meta.XR.ImmersiveDebugger.Manager
{
	internal class ActionHook : Hook
	{
		internal Action Delegate { get; private set; }

		internal ActionHook(MemberInfo memberInfo, object instance, DebugMember attribute)
			: base(memberInfo, instance, attribute)
		{
			Delegate = delegate
			{
				(memberInfo as MethodInfo)?.Invoke(instance, null);
			};
		}
	}
}
