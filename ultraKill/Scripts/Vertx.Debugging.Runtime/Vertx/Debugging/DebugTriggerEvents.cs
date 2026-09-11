using System;
using UnityEngine;

namespace Vertx.Debugging
{
	[AddComponentMenu("Debugging/Debug Trigger Events")]
	[ExecuteAlways]
	public sealed class DebugTriggerEvents : MonoBehaviour
	{
		[Flags]
		private enum Type : byte
		{
			None = 0,
			Enter = 1,
			Stay = 2,
			Exit = 4
		}

		[SerializeField]
		private Type _type = Type.Enter;

		[PairWithEnabler(null, "Duration", "_type", 1)]
		[SerializeField]
		private DebugComponentBase.ColorDurationPair _enter = new DebugComponentBase.ColorDurationPair(Shape.EnterColor, 0.1f);

		[PairWithEnabler(null, "Duration", "_type", 2)]
		[SerializeField]
		private DebugComponentBase.ColorDurationPair _stay = new DebugComponentBase.ColorDurationPair(Shape.StayColor);

		[PairWithEnabler(null, "Duration", "_type", 4)]
		[SerializeField]
		private DebugComponentBase.ColorDurationPair _exit = new DebugComponentBase.ColorDurationPair(Shape.ExitColor, 0.1f);
	}
}
