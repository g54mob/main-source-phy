using UnityEngine;

namespace BitCode.Extensions
{
	public static class GameObjectExtensions
	{
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if (!(component != null))
			{
				while (true)
				{
					uint num;
					switch ((num = 87152386u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return gameObject.AddComponent<T>();
					}
					break;
				}
			}
			return component;
		}
	}
}
