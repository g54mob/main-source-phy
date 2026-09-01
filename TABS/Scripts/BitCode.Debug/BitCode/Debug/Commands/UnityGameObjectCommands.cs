using System;
using BitCode.Attributes;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public static class UnityGameObjectCommands
	{
		[DebugCommand(Description = "Create a GameObject with the given name.")]
		public static GameObject CreateGameObject(string name = "DebugConsole-GameObject")
		{
			return new GameObject(name);
		}

		[DebugCommand(Description = "Create a GameObject with a given primitive renderer and the given name.")]
		public static GameObject CreatePrimitive(PrimitiveType primitiveType, string name = "DebugConsole-GameObject")
		{
			GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
			gameObject.name = name;
			return gameObject;
		}

		[DebugCommand(Description = "Adds a Component of the given type to the context GameObject.")]
		public static Component AddComponent(this GameObject gameObject, Type componentTypeToCreate)
		{
			return gameObject.AddComponent(componentTypeToCreate);
		}

		[DebugCommand(Description = "Gets a Component of the given type on the context GameObject.")]
		public static Component GetComponent(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponent(componentTypeToGet);
		}

		[DebugCommand(Description = "Gets all Components of the given type on the context GameObject.")]
		public static Component[] GetComponents(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponents(componentTypeToGet);
		}

		[DebugCommand(Description = "Gets the first Component of the given type on the context GameObject's children.")]
		public static Component GetComponentInChildren(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponentInChildren(componentTypeToGet);
		}

		[DebugCommand(Description = "Gets all Components of the given type on the context GameObject's children.")]
		public static Component[] GetComponentsInChildren(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponentsInChildren(componentTypeToGet);
		}

		[DebugCommand(Description = "Gets the first Component of the given type on the context GameObject's parents.")]
		public static Component GetComponentInParent(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponentInParent(componentTypeToGet);
		}

		[DebugCommand(Description = "Gets all Components of the given type on the context GameObject's parents.")]
		public static Component[] GetComponentsInParent(this GameObject gameObject, Type componentTypeToGet)
		{
			return gameObject.GetComponentsInParent(componentTypeToGet);
		}

		[DebugCommand(Description = "Toggle the active state of the context GameObject.")]
		public static void ToggleActive(this GameObject gameObject)
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}

		[DebugCommand(Description = "Set the active state of the context GameObject.")]
		public static void SetActive(this GameObject gameObject, bool active = true)
		{
			gameObject.SetActive(active);
		}

		[DebugCommand(Description = "Prints the context GameObject's name.")]
		public static string PrintName(this GameObject contextGameObject)
		{
			return contextGameObject.name;
		}

		[DebugCommand(Description = "Sets the context GameObject's name to the given value.")]
		public static void SetName(this GameObject contextGameObject, string newName)
		{
			contextGameObject.name = newName;
		}

		[DebugCommand(Description = "Gets the context GameObject's transform.")]
		public static Transform GetTransform(this GameObject contextGameObject)
		{
			return contextGameObject.transform;
		}
	}
}
