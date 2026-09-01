using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMDebugMenuButtonEvent
	{
		public enum EventModes
		{
			FromButton = 0,
			SetButton = 1
		}

		public delegate void Delegate(string buttonEventName, bool active = true, EventModes eventMode = EventModes.FromButton);

		private static event Delegate OnEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialization()
		{
		}

		public static void Register(Delegate callback)
		{
		}

		public static void Unregister(Delegate callback)
		{
		}

		public static void Trigger(string buttonEventName, bool active = true, EventModes eventMode = EventModes.FromButton)
		{
		}
	}
}
