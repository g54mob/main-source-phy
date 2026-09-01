using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class GridCellCounter : MonoBehaviour
	{
		[Serializable]
		public class CellCountChanged : UnityEvent<int>
		{
		}

		public CellCountChanged onCellCountChanged;

		private Rect m_lastDimensions = new Rect
		{
			x = -1f,
			y = -1f
		};

		private int m_lastCellCount = -1;

		public GridLayoutGroup grid
		{
			get
			{
				return GetComponent<GridLayoutGroup>();
			}
		}

		private RectTransform rectTransform
		{
			get
			{
				return (RectTransform)base.transform;
			}
		}

		private void Start()
		{
			OnGUI();
		}

		private void OnEnable()
		{
			OnGUI();
		}

		private void OnGUI()
		{
			if (m_lastDimensions != rectTransform.rect)
			{
				int num = UIUtilities.CalculateGridCellCount(grid);
				if (num != m_lastCellCount && onCellCountChanged != null)
				{
					onCellCountChanged.Invoke(num);
				}
				m_lastCellCount = num;
				m_lastDimensions = rectTransform.rect;
			}
		}
	}
}
