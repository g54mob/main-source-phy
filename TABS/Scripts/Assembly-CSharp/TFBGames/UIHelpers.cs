using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public static class UIHelpers
	{
		public static IList<Selectable> GetSelectableChildren(this Transform parent, bool excludeInactive = false)
		{
			List<Selectable> list = null;
			if (parent != null && parent.childCount > 0)
			{
				list = new List<Selectable>();
				foreach (Transform item in parent)
				{
					if (!excludeInactive || !(item != null) || !item.gameObject.activeSelf)
					{
						Selectable component = item.GetComponent<Selectable>();
						if (component != null)
						{
							list.Add(component);
						}
					}
				}
			}
			return list;
		}

		public static void CreateAutomaticNavigation(IList<Selectable> selectables)
		{
			if (selectables == null || selectables.Count <= 0)
			{
				return;
			}
			Navigation navigation = new Navigation
			{
				mode = Navigation.Mode.Automatic
			};
			foreach (Selectable selectable in selectables)
			{
				if (selectable.IsInteractable() && selectable.gameObject.activeSelf)
				{
					selectable.navigation = navigation;
				}
			}
		}

		public static void CreateExplicitLinearNavigation(IList<Selectable> selectables, bool horizontal)
		{
			int num = selectables?.Count ?? 0;
			if (num <= 0)
			{
				return;
			}
			Navigation navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit
			};
			List<Selectable> list = new List<Selectable>();
			foreach (Selectable selectable6 in selectables)
			{
				selectable6.navigation = navigation;
				if (selectable6.IsInteractable() && selectable6.gameObject.activeSelf)
				{
					list.Add(selectable6);
				}
			}
			num = list.Count;
			if (num <= 0)
			{
				return;
			}
			Selectable selectable = list[0];
			Selectable selectable2 = null;
			for (int i = 0; i < num; i++)
			{
				Selectable selectable3 = list[i];
				Navigation navigation2 = (selectable3.navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit
				});
				selectable2 = selectable3;
				int index = ((i == 0) ? (num - 1) : (i - 1));
				int index2 = ((i != num - 1) ? (i + 1) : 0);
				Selectable selectable4 = list[index];
				Selectable selectable5 = list[index2];
				if (horizontal)
				{
					navigation2.selectOnLeft = selectable4;
					navigation2.selectOnRight = selectable5;
				}
				else
				{
					navigation2.selectOnUp = selectable4;
					navigation2.selectOnDown = selectable5;
				}
				selectable3.navigation = navigation2;
			}
			if (selectable2 != null && (selectable2.navigation.selectOnDown == null || selectable2.navigation.selectOnRight == null))
			{
				Navigation navigation4 = selectable2.navigation;
				Navigation navigation5 = selectable.navigation;
				if (horizontal)
				{
					navigation4.selectOnRight = selectable;
					navigation5.selectOnLeft = selectable2;
				}
				else
				{
					navigation4.selectOnDown = selectable;
					navigation5.selectOnUp = selectable2;
				}
				selectable2.navigation = navigation4;
				selectable.navigation = navigation5;
			}
		}

		public static Vector2 WorldToUISpace(this RectTransform target, Camera viewportCamera, Vector3 worldPosition)
		{
			Vector3 vector = viewportCamera.WorldToScreenPoint(worldPosition);
			Vector2 result = target.position;
			result.x = vector.x;
			result.y = vector.y;
			return result;
		}
	}
}
