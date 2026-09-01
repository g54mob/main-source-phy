using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ModIO.UI
{
	public static class UIUtilities
	{
		public static Sprite CreateSpriteFromTexture(Texture2D texture)
		{
			return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
		}

		public static void OpenYouTubeVideoURL(string youTubeVideoId)
		{
			if (!string.IsNullOrEmpty(youTubeVideoId))
			{
				Application.OpenURL("https://youtu.be/" + youTubeVideoId);
			}
		}

		public static int CalculateGridCellCount(GridLayoutGroup gridLayout)
		{
			RectTransform component = gridLayout.GetComponent<RectTransform>();
			Vector2 vector = new Vector2
			{
				x = component.rect.width - (float)gridLayout.padding.left - (float)gridLayout.padding.right + gridLayout.spacing.x,
				y = component.rect.height - (float)gridLayout.padding.top - (float)gridLayout.padding.bottom + gridLayout.spacing.y
			};
			int num = 0;
			if (gridLayout.cellSize.x + gridLayout.spacing.x > 0f)
			{
				num = (int)Mathf.Floor(vector.x / (gridLayout.cellSize.x + gridLayout.spacing.x));
			}
			int num2 = 0;
			if (gridLayout.cellSize.y + gridLayout.spacing.y > 0f)
			{
				num2 = (int)Mathf.Floor(vector.y / (gridLayout.cellSize.y + gridLayout.spacing.y));
			}
			if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				if (gridLayout.constraintCount < num)
				{
					num = gridLayout.constraintCount;
				}
			}
			else if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedRowCount && gridLayout.constraintCount < num2)
			{
				num2 = gridLayout.constraintCount;
			}
			return num2 * num;
		}

		public static int CalculateGridColumnCount(GridLayoutGroup gridLayout)
		{
			float x = ((RectTransform)gridLayout.transform).rect.size.x;
			int num = 1;
			if (gridLayout.cellSize.x + gridLayout.spacing.x <= 0f)
			{
				num = int.MaxValue;
			}
			else
			{
				float num2 = x - (float)gridLayout.padding.horizontal + 0.001f;
				float num3 = gridLayout.cellSize.x + gridLayout.spacing.x;
				num = Mathf.Max(1, Mathf.FloorToInt((num2 + gridLayout.spacing.x) / num3));
			}
			if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedColumnCount && num > gridLayout.constraintCount)
			{
				num = gridLayout.constraintCount;
			}
			return num;
		}

		public static T FindComponentInAllScenes<T>(bool includeInactive) where T : Behaviour
		{
			T[] array = Resources.FindObjectsOfTypeAll<T>();
			foreach (T val in array)
			{
				if (val.hideFlags != HideFlags.NotEditable && val.hideFlags != HideFlags.HideAndDontSave && (includeInactive || val.isActiveAndEnabled))
				{
					return val;
				}
			}
			return null;
		}

		public static List<T> FindComponentsInAllScenes<T>(bool includeInactive) where T : Behaviour
		{
			List<T> list = new List<T>();
			T[] array = Resources.FindObjectsOfTypeAll<T>();
			foreach (T val in array)
			{
				if (val.hideFlags != HideFlags.NotEditable && val.hideFlags != HideFlags.HideAndDontSave && (includeInactive || val.isActiveAndEnabled))
				{
					list.Add(val);
				}
			}
			return list;
		}

		public static void SetExplicitGridNavigation(IList<Selectable> selectables, int columnCount, EdgeCellNavigationMode horizontalNavigationStyle, EdgeCellNavigationMode verticalNavigationStyle)
		{
			if (selectables == null || selectables.Count == 0)
			{
				return;
			}
			if (columnCount < 1)
			{
				columnCount = 1;
			}
			if (columnCount > selectables.Count)
			{
				columnCount = selectables.Count;
			}
			int rowCount = (selectables.Count + columnCount - 1) / columnCount;
			Func<int, int> getCol = (int gridIndex) => gridIndex % columnCount;
			Func<int, int> getRow = (int gridIndex) => gridIndex / columnCount;
			Func<int, int, int> getGridIndex = (int col, int row) => row * columnCount + col;
			Func<int, Selectable> func = (int gridIndex) => (Selectable)null;
			Func<int, Selectable> func2 = (int gridIndex) => (Selectable)null;
			Func<int, Selectable> func3 = (int gridIndex) => (Selectable)null;
			Func<int, Selectable> func4 = (int gridIndex) => (Selectable)null;
			switch (horizontalNavigationStyle)
			{
			case EdgeCellNavigationMode.Wrap:
				func = delegate(int gridIndex)
				{
					int arg = getRow(gridIndex);
					int num4 = columnCount - 1;
					while (getGridIndex(num4, arg) > selectables.Count)
					{
						num4--;
					}
					return selectables[getGridIndex(num4, arg)];
				};
				func2 = delegate(int gridIndex)
				{
					int arg = getRow(gridIndex);
					return selectables[getGridIndex(0, arg)];
				};
				break;
			case EdgeCellNavigationMode.WrapAndIncrement:
				func = delegate(int gridIndex)
				{
					int num4 = getRow(gridIndex) - 1;
					if (num4 < 0)
					{
						num4 = rowCount - 1;
					}
					int num5 = columnCount - 1;
					while (getGridIndex(num5, num4) > selectables.Count)
					{
						num5--;
					}
					return selectables[getGridIndex(num5, num4)];
				};
				func2 = delegate(int gridIndex)
				{
					int num4 = getRow(gridIndex) + 1;
					if (num4 >= rowCount)
					{
						num4 = 0;
					}
					return selectables[getGridIndex(0, num4)];
				};
				break;
			}
			switch (verticalNavigationStyle)
			{
			case EdgeCellNavigationMode.Wrap:
				func3 = delegate(int gridIndex)
				{
					int arg = getCol(gridIndex);
					int num4 = rowCount - 1;
					while (getGridIndex(arg, num4) > selectables.Count)
					{
						num4--;
					}
					return selectables[getGridIndex(arg, num4)];
				};
				func4 = delegate(int gridIndex)
				{
					int arg = getCol(gridIndex);
					return selectables[getGridIndex(arg, 0)];
				};
				break;
			case EdgeCellNavigationMode.WrapAndIncrement:
				func3 = delegate(int gridIndex)
				{
					int num4 = getCol(gridIndex) - 1;
					if (num4 < 0)
					{
						num4 = columnCount - 1;
					}
					int num5 = rowCount - 1;
					while (getGridIndex(num4, num5) > selectables.Count)
					{
						num5--;
					}
					return selectables[getGridIndex(num4, num5)];
				};
				func4 = delegate(int gridIndex)
				{
					int num4 = getCol(gridIndex) + 1;
					if (num4 >= columnCount)
					{
						num4 = 0;
					}
					return selectables[getGridIndex(num4, 0)];
				};
				break;
			}
			for (int num = 0; num < selectables.Count; num++)
			{
				Selectable selectable = selectables[num];
				Navigation navigation = new Navigation
				{
					mode = Navigation.Mode.Explicit
				};
				int num2 = getCol(num);
				int num3 = getRow(num);
				if (num2 > 0)
				{
					navigation.selectOnLeft = selectables[num - 1];
				}
				else
				{
					navigation.selectOnLeft = func(num);
				}
				if (num2 < columnCount - 1 && num < selectables.Count - 1)
				{
					navigation.selectOnRight = selectables[num + 1];
				}
				else
				{
					navigation.selectOnRight = func2(num);
				}
				if (num3 > 0)
				{
					navigation.selectOnUp = selectables[getGridIndex(num2, num3 - 1)];
				}
				else
				{
					navigation.selectOnUp = func3(num);
				}
				if (num3 < rowCount - 1)
				{
					navigation.selectOnDown = selectables[getGridIndex(num2, num3 + 1)];
				}
				else
				{
					navigation.selectOnDown = func4(num);
				}
				selectable.navigation = navigation;
			}
		}

		public static void SetInstanceCount<T>(Transform container, T template, string instanceName, int instanceCount, ref T[] instanceArray, bool reactivateAll = false) where T : MonoBehaviour
		{
			if (instanceArray == null)
			{
				instanceArray = new T[0];
			}
			if (instanceCount - instanceArray.Length != 0)
			{
				T[] array = new T[instanceCount];
				for (int i = 0; i < instanceArray.Length && i < instanceCount; i++)
				{
					array[i] = instanceArray[i];
				}
				for (int j = instanceArray.Length; j < instanceCount; j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(template.gameObject);
					gameObject.name = instanceName + " [" + j.ToString("00") + "]";
					gameObject.transform.SetParent(container, worldPositionStays: false);
					gameObject.SetActive(value: true);
					array[j] = gameObject.GetComponent<T>();
				}
				for (int k = instanceCount; k < instanceArray.Length; k++)
				{
					UnityEngine.Object.Destroy(instanceArray[k].gameObject);
				}
				instanceArray = array;
			}
			if (reactivateAll)
			{
				T[] array2 = instanceArray;
				foreach (T val in array2)
				{
					val.gameObject.SetActive(value: false);
					val.gameObject.SetActive(value: true);
				}
			}
		}

		[Obsolete("Use UIUtilities.FindComponentInAllScenes() instead.")]
		public static T FindComponentInScene<T>(bool includeInactive) where T : class
		{
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			T val = null;
			foreach (GameObject item in (IEnumerable<GameObject>)rootGameObjects)
			{
				if (includeInactive || item.activeInHierarchy)
				{
					val = item.GetComponent<T>();
					if (val != null)
					{
						return val;
					}
					val = item.GetComponentInChildren<T>(includeInactive);
					if (val != null)
					{
						return val;
					}
				}
			}
			return null;
		}

		[Obsolete("Use UIUtilities.FindComponentsInLoadedScenes() instead.")]
		public static List<T> FindComponentsInScene<T>(bool includeInactive) where T : class
		{
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			List<T> list = new List<T>();
			foreach (GameObject item in (IEnumerable<GameObject>)rootGameObjects)
			{
				if (includeInactive || item.activeInHierarchy)
				{
					list.AddRange(item.GetComponents<T>());
					list.AddRange(item.GetComponentsInChildren<T>(includeInactive));
				}
			}
			return list;
		}

		[Obsolete("Renamed to CalculateGridCellCount.")]
		public static int CountVisibleGridCells(GridLayoutGroup gridLayout)
		{
			return CalculateGridCellCount(gridLayout);
		}
	}
}
