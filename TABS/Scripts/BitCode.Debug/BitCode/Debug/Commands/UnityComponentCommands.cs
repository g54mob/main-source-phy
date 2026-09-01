using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public static class UnityComponentCommands
	{
		[DebugCommand(Description = "Gets this component's GameObject.")]
		public static GameObject GetGameObject(this Component component)
		{
			return component.gameObject;
		}
	}
}
