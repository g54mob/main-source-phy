using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTHandles
{
	public class RuntimeSelectionComponentUI : Selectable
	{
		[SerializeField]
		private Image m_background;

		private bool m_isSelected;

		public bool IsSelected
		{
			get
			{
				return m_isSelected;
			}
			private set
			{
				if (m_isSelected == value)
				{
					return;
				}
				m_isSelected = value;
				if (m_isSelected)
				{
					if (this.Selected != null)
					{
						this.Selected(this, EventArgs.Empty);
					}
				}
				else if (this.Unselected != null)
				{
					this.Unselected(this, EventArgs.Empty);
				}
			}
		}

		public event EventHandler Selected;

		public event EventHandler Unselected;

		protected override void Awake()
		{
			base.Awake();
			if (m_background == null)
			{
				base.gameObject.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			}
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			IsSelected = true;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			IsSelected = false;
		}
	}
}
