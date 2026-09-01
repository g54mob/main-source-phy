using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(LayoutGroup))]
	public class LayouterNavigationAutomator : UIBehaviour
	{
		public EdgeCellNavigationMode horizontalNavigation;

		public EdgeCellNavigationMode verticalNavigation;

		public int selectableDepth = 1;

		protected override void Start()
		{
			base.Start();
			UpdateNavigationForChildren();
		}

		private void OnTransformChildrenChanged()
		{
			UpdateNavigationForChildren();
		}

		public void UpdateNavigationForChildren()
		{
			LayoutGroup component = GetComponent<LayoutGroup>();
			if (component == null)
			{
				return;
			}
			int columnCount = 1;
			List<Selectable> selectables = null;
			if (selectableDepth < 0)
			{
				selectables = new List<Selectable>(base.gameObject.GetComponentsInChildren<Selectable>());
			}
			else
			{
				selectables = new List<Selectable>();
				Action<Transform, int> appendChildSelectables = null;
				appendChildSelectables = delegate(Transform t, int depth)
				{
					ILayoutIgnorer[] components = t.gameObject.GetComponents<ILayoutIgnorer>();
					for (int i = 0; i < components.Length; i++)
					{
						if (components[i].ignoreLayout)
						{
							return;
						}
					}
					if (t.gameObject.activeSelf)
					{
						if (depth != selectableDepth)
						{
							foreach (Transform item in t)
							{
								appendChildSelectables(item, depth + 1);
							}
							return;
						}
						Selectable component2 = t.gameObject.GetComponent<Selectable>();
						if (component2 != null)
						{
							selectables.Add(component2);
						}
					}
				};
				appendChildSelectables(base.transform, 0);
			}
			if (component is HorizontalLayoutGroup)
			{
				columnCount = selectables.Count;
			}
			else if (component is GridLayoutGroup)
			{
				columnCount = UIUtilities.CalculateGridColumnCount((GridLayoutGroup)component);
			}
			UIUtilities.SetExplicitGridNavigation(selectables, columnCount, horizontalNavigation, verticalNavigation);
		}
	}
}
