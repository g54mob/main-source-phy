using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public static class RuntimeSelectionUtil
	{
		private static HashSet<Object> m_rootsHs = new HashSet<Object>();

		public static Object[] GetRoots(IList<Object> objects)
		{
			if (objects == null)
			{
				return null;
			}
			if (objects.Count == 0)
			{
				return objects.ToArray();
			}
			if (objects.Count != 1)
			{
				for (int i = 0; i < objects.Count; i++)
				{
					m_rootsHs.Add(GetRoot(objects[i]));
				}
				Object[] result = m_rootsHs.ToArray();
				m_rootsHs.Clear();
				return result;
			}
			return new Object[1] { GetRoot(objects[0]) };
		}

		private static Object GetRoot(Object obj)
		{
			GameObject gameObject = obj as GameObject;
			if (gameObject == null)
			{
				Component component = obj as Component;
				if (!component)
				{
					return obj;
				}
				gameObject = component.gameObject;
			}
			while (gameObject.transform.parent != null && gameObject.transform.parent.tag != ExposeToEditor.HierarchyRootTag)
			{
				gameObject = gameObject.transform.parent.gameObject;
			}
			return gameObject;
		}
	}
}
