using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public static class UnityTransformCommands
	{
		[DebugCommand(Description = "Prints all the context Transform's children.")]
		public static void PrintChildren(this Transform parent, IDebugConsoleWriter writer)
		{
			writer.AppendLine($"Printing {parent.childCount} children.");
			int num3 = default(int);
			while (true)
			{
				int num = 771425164;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x12C4BBC7)) % 6)
					{
					case 4u:
						break;
					default:
						return;
					case 2u:
						writer.AppendLine(parent.GetChild(num3).name);
						num3++;
						num = 779742239;
						continue;
					case 0u:
					{
						int num4;
						if (num3 < parent.childCount)
						{
							num = 628640203;
							num4 = num;
						}
						else
						{
							num = 1204838316;
							num4 = num;
						}
						continue;
					}
					case 1u:
						num3 = 0;
						num = (int)((num2 * 986863492) ^ 0x545305CE);
						continue;
					case 3u:
						num = ((int)num2 * -1217053377) ^ 0x33AA9084;
						continue;
					case 5u:
						return;
					}
					break;
				}
			}
		}

		[DebugCommand(Description = "Creates a new GameObject under the context Transform, with the given name.")]
		public static GameObject CreateGameObjectUnder(this Transform parent, string name = "DebugConsole-GameObject")
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.SetParent(parent, worldPositionStays: false);
			return gameObject;
		}

		[DebugCommand(Description = "Moves the context Transform to the given world position.")]
		public static void MoveTo(this Transform transform, Vector3 newPosition)
		{
			transform.position = newPosition;
		}

		[DebugCommand(Description = "Sets the context Transform to the given world-space rotation, specified in Euler angles.")]
		public static void RotateTo(this Transform transform, Vector3 newEulerAngles)
		{
			transform.eulerAngles = newEulerAngles;
		}

		[DebugCommand(Description = "Moves the context Transform to the given local position.")]
		public static void MoveToLocal(this Transform transform, Vector3 newPosition)
		{
			transform.localPosition = newPosition;
		}

		[DebugCommand(Description = "Sets the context Transform to the given local-space rotation, specified in Euler angles.")]
		public static void RotateToLocal(this Transform transform, Vector3 newEulerAngles)
		{
			transform.localEulerAngles = newEulerAngles;
		}

		[DebugCommand(Description = "Sets the context Transform's local space to the given value.")]
		public static void ScaleToLocal(this Transform transform, Vector3 newScale)
		{
			transform.localScale = newScale;
		}

		[DebugCommand(Description = "Gets this Transform's parent Transform.")]
		public static Transform GetParent(this Transform transform)
		{
			return transform.parent;
		}

		[DebugCommand(Description = "Finds a child Transform of the context Transform.")]
		public static Transform FindChild(this Transform contextTransform, string childPath)
		{
			return contextTransform.Find(childPath);
		}
	}
}
