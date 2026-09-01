using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Poly.Extension
{
	public static class ComponentExtension
	{
		private static StringBuilder builder = new StringBuilder();

		private static List<Transform> hierarchy = new List<Transform>();

		public static string GetFullName<T>(this T component, int numParents = 1) where T : Component
		{
			hierarchy.Clear();
			Transform transform = component.transform;
			do
			{
				hierarchy.Add(transform);
				transform = transform.parent;
			}
			while ((bool)transform && 0 < numParents--);
			builder.Clear();
			int num = hierarchy.Count - 1;
			while (0 < num)
			{
				builder.Append(hierarchy[num].name);
				builder.Append(" | ");
				num--;
			}
			builder.Append(component.name);
			return builder.ToString();
		}
	}
}
