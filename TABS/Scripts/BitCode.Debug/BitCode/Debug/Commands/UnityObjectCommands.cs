using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public static class UnityObjectCommands
	{
		[DebugCommand(Description = "Destroys the context Unity Object.")]
		public static void Destroy(this Object unityObject, DebugConsole console)
		{
			Object.Destroy(unityObject);
		}
	}
}
