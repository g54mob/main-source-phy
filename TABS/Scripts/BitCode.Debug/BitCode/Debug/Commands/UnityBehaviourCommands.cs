using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public static class UnityBehaviourCommands
	{
		[DebugCommand(Description = "Toggle the enabled state of this Behaviour.")]
		public static void ToggleEnabled(this Behaviour behaviour)
		{
			behaviour.enabled = !behaviour.enabled;
		}

		[DebugCommand(Description = "Set the enabled state of this Behaviour.")]
		public static void SetEnabled(this Behaviour behaviour, bool enabled = true)
		{
			behaviour.enabled = enabled;
		}
	}
}
