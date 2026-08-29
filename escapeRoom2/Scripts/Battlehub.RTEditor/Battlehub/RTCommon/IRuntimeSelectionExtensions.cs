using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public static class IRuntimeSelectionExtensions
	{
		private static readonly ExposeToEditor[] s_emptyExposeToEditor = new ExposeToEditor[0];

		private static readonly Transform[] s_emptyTransform = new Transform[0];

		public static IList<Transform> GetTransforms(this IRuntimeSelection selection)
		{
			if (selection.activeGameObject == null)
			{
				return s_emptyTransform;
			}
			return selection.GetComponents<Transform>();
		}

		public static IList<ExposeToEditor> GetExposedToEditor(this IRuntimeSelection selection)
		{
			if (selection.activeGameObject == null)
			{
				return s_emptyExposeToEditor;
			}
			return selection.GetComponents<ExposeToEditor>();
		}

		public static IList<T> GetComponents<T>(this IRuntimeSelection selection) where T : Component
		{
			if (selection.activeGameObject == null)
			{
				return new T[0];
			}
			List<T> list = new List<T>();
			for (int i = 0; i < selection.objects.Length; i++)
			{
				GameObject gameObject = selection.objects[i] as GameObject;
				if (gameObject != null)
				{
					T component = gameObject.GetComponent<T>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			return list;
		}
	}
}
