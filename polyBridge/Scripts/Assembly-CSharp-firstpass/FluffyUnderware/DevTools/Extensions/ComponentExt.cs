using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class ComponentExt
	{
		public static void StripComponents(this Component c, params Type[] toKeep)
		{
			if (toKeep.Length == 0)
			{
				c.gameObject.StripComponents(c.GetType());
			}
			else
			{
				c.gameObject.StripComponents(toKeep);
			}
		}

		public static GameObject AddChildGameObject(this Component c, string name)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.SetParent(c.transform);
			return gameObject;
		}

		public static T AddChildGameObject<T>(this Component c, string name) where T : Component
		{
			GameObject gameObject = new GameObject(name);
			if ((bool)gameObject)
			{
				gameObject.transform.SetParent(c.transform);
				return gameObject.AddComponent<T>();
			}
			return null;
		}

		public static T DuplicateGameObject<T>(this Component source, Transform newParent, bool keepPrefabConnection = false) where T : Component
		{
			if (!source || !source.gameObject)
			{
				return null;
			}
			int num = new List<Component>(source.gameObject.GetComponents<Component>()).IndexOf(source);
			GameObject gameObject = UnityEngine.Object.Instantiate(source.gameObject);
			if ((bool)gameObject)
			{
				gameObject.transform.SetParent(newParent, worldPositionStays: false);
				return gameObject.GetComponents<Component>()[num] as T;
			}
			return null;
		}

		public static Component DuplicateGameObject(this Component source, Transform newParent, bool keepPrefabConnection = false)
		{
			if (!source || !source.gameObject || !newParent)
			{
				return null;
			}
			int num = new List<Component>(source.gameObject.GetComponents<Component>()).IndexOf(source);
			GameObject gameObject = UnityEngine.Object.Instantiate(source.gameObject);
			if ((bool)gameObject)
			{
				gameObject.transform.SetParent(newParent, worldPositionStays: false);
				return gameObject.GetComponents<Component>()[num];
			}
			return null;
		}
	}
}
