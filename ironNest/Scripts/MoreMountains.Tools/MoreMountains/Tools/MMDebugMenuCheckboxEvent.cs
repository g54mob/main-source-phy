using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMDebugMenuCheckboxEvent
	{
		public enum EventModes
		{
			FromCheckbox = 0,
			SetCheckbox = 1
		}

		public delegate void Delegate(string checkboxEventName, bool value, EventModes eventMode = EventModes.FromCheckbox);

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

		public static void Trigger(string checkboxEventName, bool value, EventModes eventMode = EventModes.FromCheckbox)
		{
		}
	}
}
