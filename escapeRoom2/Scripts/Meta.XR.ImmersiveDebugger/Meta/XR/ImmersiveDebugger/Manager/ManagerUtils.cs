using System;
using System.Collections.Generic;
using System.Reflection;
using Meta.XR.ImmersiveDebugger.UserInterface;
using Meta.XR.ImmersiveDebugger.Utils;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.Manager
{
	internal static class ManagerUtils
	{
		public delegate void RegisterMember<in T>(IMember memberController, T member, DebugMember attribute, UnityEngine.Object instance);

		public static void RebuildInspectorForType<T>(IDebugUIPanel panel, InstanceCache cache, Type type, List<(T, DebugMember)> memberPairs, RegisterMember<T> memberRegistration) where T : MemberInfo
		{
			foreach (var (val, debugMember) in memberPairs)
			{
				if (val.IsStatic())
				{
					IMember member = panel.RegisterInspector(InstanceHandle.Static(type), debugMember.Category)?.RegisterMember(val, debugMember);
					if (member != null)
					{
						memberRegistration(member, val, debugMember, null);
					}
					continue;
				}
				foreach (InstanceHandle item in cache.GetCacheDataForClass(type))
				{
					IMember member2 = panel.RegisterInspector(item, debugMember.Category)?.RegisterMember(val, debugMember);
					if (member2 != null)
					{
						memberRegistration(member2, val, debugMember, item.Instance);
					}
				}
			}
		}
	}
}
