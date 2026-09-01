using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSelectableItem : Selectable, ISubmitHandler, IEventSystemHandler
	{
		[SerializeField]
		[Tooltip("(Optional) The button that mouse users click to execute the main action.")]
		protected Button correspondingButton;

		[SerializeField]
		protected UnityEvent onSubmitEvent;

		private ActionGlyph[] glyphs;

		protected bool isSelected;

		public event Action<UnitEditorSelectableItem> Submitted;

		public event Action<UnitEditorSelectableItem> Selected;

		public event Action<UnitEditorSelectableItem> Deselected;

		protected override void Start()
		{
			base.Start();
			glyphs = GetComponentsInChildren<ActionGlyph>(includeInactive: true);
			SetGlyphsActive(active: false);
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.Selected?.Invoke(this);
			SetGlyphsActive(active: true);
			isSelected = true;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.Deselected?.Invoke(this);
			SetGlyphsActive(active: false);
			isSelected = false;
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (!UnitEditorManager.isTestingUnit)
			{
				if (correspondingButton != null)
				{
					correspondingButton.OnSubmit(eventData);
				}
				if (onSubmitEvent != null)
				{
					onSubmitEvent.Invoke();
				}
				this.Submitted?.Invoke(this);
			}
		}

		protected void SetGlyphsActive(bool active)
		{
			if (glyphs != null && glyphs.Length != 0)
			{
				ActionGlyph[] array = glyphs;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].gameObject.SetActive(active);
				}
			}
		}
	}
}
