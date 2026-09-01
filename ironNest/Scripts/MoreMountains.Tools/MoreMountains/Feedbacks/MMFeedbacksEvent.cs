using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct MMFeedbacksEvent
	{
		public enum EventTypes
		{
			Play = 0,
			Pause = 1,
			Resume = 2,
			Revert = 3,
			Complete = 4,
			SkipToTheEnd = 5,
			RestoreInitialValues = 6,
			Loop = 7,
			Enable = 8,
			Disable = 9,
			InitializationComplete = 10
		}

		public delegate void Delegate(MMFeedbacks source, EventTypes type);

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

		public static void Trigger(MMFeedbacks source, EventTypes type)
		{
		}
	}
}
