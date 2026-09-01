using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMDebugMenuSliderEvent
	{
		public enum EventModes
		{
			FromSlider = 0,
			SetSlider = 1
		}

		public delegate void Delegate(string sliderEventName, float value, EventModes eventMode = EventModes.FromSlider);

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

		public static void Trigger(string sliderEventName, float value, EventModes eventMode = EventModes.FromSlider)
		{
		}
	}
}
