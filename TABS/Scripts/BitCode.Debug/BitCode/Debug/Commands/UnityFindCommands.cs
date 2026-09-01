using System;
using BitCode.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BitCode.Debug.Commands
{
	public static class UnityFindCommands
	{
		[DebugCommand(Description = "Finds a Unity object of the given type.")]
		public static UnityEngine.Object FindObjectOfType(Type type)
		{
			return UnityEngine.Object.FindObjectOfType(type);
		}

		[DebugCommand(Description = "Finds all active loaded Unity objects of the given type.")]
		public static UnityEngine.Object[] FindObjectsOfType(Type type)
		{
			return UnityEngine.Object.FindObjectsOfType(type);
		}

		[DebugCommand(Description = "Finds a game object with the given name. Also supports transform hierarchy paths, separated by '/'.")]
		public static GameObject FindGameObject(string name)
		{
			int num = name.IndexOf('/');
			Transform transform = default(Transform);
			GameObject gameObject = default(GameObject);
			int num4 = default(int);
			GameObject[] rootGameObjects = default(GameObject[]);
			string b = default(string);
			while (true)
			{
				int num2 = -1364004168;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -482034523)) % 14)
					{
					case 11u:
						break;
					case 4u:
						transform = gameObject.transform.Find(name.Substring(num + 1));
						num2 = (int)(num3 * 816943874) ^ -884006049;
						continue;
					case 6u:
					{
						int num6;
						if (num4 < rootGameObjects.Length)
						{
							num2 = -1878340173;
							num6 = num2;
						}
						else
						{
							num2 = -938179327;
							num6 = num2;
						}
						continue;
					}
					case 5u:
						return GameObject.Find(name);
					case 8u:
						return null;
					case 12u:
						num4++;
						num2 = -1332212323;
						continue;
					case 7u:
					{
						int num9;
						int num10;
						if (num < 0)
						{
							num9 = -528136475;
							num10 = num9;
						}
						else
						{
							num9 = -1004377153;
							num10 = num9;
						}
						num2 = num9 ^ (int)(num3 * 2004156679);
						continue;
					}
					case 13u:
						return transform.gameObject;
					case 2u:
					{
						int num7;
						int num8;
						if (transform != null)
						{
							num7 = -915786614;
							num8 = num7;
						}
						else
						{
							num7 = -751038077;
							num8 = num7;
						}
						num2 = num7 ^ ((int)num3 * -2021958320);
						continue;
					}
					case 10u:
					{
						gameObject = rootGameObjects[num4];
						int num5;
						if (string.Equals(gameObject.name, b, StringComparison.InvariantCulture))
						{
							num2 = -1753167987;
							num5 = num2;
						}
						else
						{
							num2 = -347489821;
							num5 = num2;
						}
						continue;
					}
					case 9u:
						rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
						num2 = (int)((num3 * 1272046251) ^ 0x389B4D69);
						continue;
					case 1u:
						b = name.Substring(0, num);
						num2 = -1709801746;
						continue;
					case 3u:
						num4 = 0;
						num2 = ((int)num3 * -860742786) ^ 0x34074C4B;
						continue;
					default:
						return null;
					}
					break;
				}
			}
		}

		[DebugCommand(Description = "Finds a GameObject with the given tag.")]
		public static GameObject FindGameObjectWithTag(string tag)
		{
			return GameObject.FindWithTag(tag);
		}

		[DebugCommand(Description = "Finds all GameObjects with the given tag.")]
		public static GameObject[] FindGameObjectsWithTag(string tag)
		{
			return GameObject.FindGameObjectsWithTag(tag);
		}
	}
}
