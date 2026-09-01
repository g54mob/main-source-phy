using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameObjectHelper
{
	public static List<T> FindObjectsOfTypeAll<T>()
	{
		List<T> list = new List<T>();
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (sceneAt.isLoaded)
			{
				GameObject[] rootGameObjects = sceneAt.GetRootGameObjects();
				foreach (GameObject gameObject in rootGameObjects)
				{
					list.AddRange(gameObject.GetComponentsInChildren<T>(true));
				}
			}
		}
		return list;
	}

	public static bool IsColliderNegativelyScaled(Collider collider)
	{
		bool result = false;
		if (collider is BoxCollider)
		{
			result = IsVectorNegative(((BoxCollider)collider).size);
		}
		else if (collider is SphereCollider)
		{
			result = ((SphereCollider)collider).radius < 0f;
		}
		else if (collider is CapsuleCollider)
		{
			result = ((CapsuleCollider)collider).radius < 0f || ((CapsuleCollider)collider).height < 0f;
		}
		return result;
	}

	public static bool IsGameObjectNegativelyScaled(GameObject gameObject)
	{
		return IsVectorNegative(gameObject.transform.localScale);
	}

	public static bool IsVectorNegative(Vector3 vector)
	{
		return vector.x < 0f || vector.y < 0f || vector.z < 0f;
	}

	public static string GetGameObjectPath(this GameObject gameObject)
	{
		List<string> list = new List<string>();
		Transform transform = gameObject.transform;
		while (transform != null)
		{
			list.Add(transform.name);
			transform = transform.transform.parent;
		}
		list.Reverse();
		return string.Join("/", list.ToArray());
	}
}
