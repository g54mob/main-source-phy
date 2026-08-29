using System;
using System.Reflection;

namespace Meta.XR.ImmersiveDebugger.Manager
{
	internal class GizmoHook : Hook
	{
		public Action<bool> SetState { get; }

		public Func<bool> GetState { get; }

		public GizmoHook(MemberInfo memberInfo, object instance, DebugMember attribute, Action<bool> setState, Func<bool> getState)
			: base(memberInfo, instance, attribute)
		{
			SetState = setState;
			GetState = getState;
			SetState?.Invoke(attribute.ShowGizmoByDefault);
		}
	}
}
