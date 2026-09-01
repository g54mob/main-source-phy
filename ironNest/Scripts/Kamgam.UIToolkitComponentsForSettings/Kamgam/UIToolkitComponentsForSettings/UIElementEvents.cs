using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Kamgam.UIToolkitComponentsForSettings
{
	public class UIElementEvents : UIElementClickEvent
	{
		[Header("Events")]
		public UnityEvent<PointerDownEvent> OnPointerDown;

		public UnityEvent<PointerUpEvent> OnPointerUp;

		public UnityEvent<PointerEnterEvent> OnPointerEnter;

		public UnityEvent<PointerLeaveEvent> OnPointerLeave;

		public UnityEvent<FocusEvent> OnFocus;

		public UnityEvent<BlurEvent> OnBlur;

		public UnityEvent<KeyDownEvent> OnKeyDown;

		public UnityEvent<KeyUpEvent> OnKeyUp;

		public UnityEvent<ChangeEvent<bool>> OnChangeBool;

		public UnityEvent<ChangeEvent<int>> OnChangeInt;

		public UnityEvent<ChangeEvent<float>> OnChangeFloat;

		public UnityEvent<ChangeEvent<string>> OnChangeString;

		public override void RegisterEvents()
		{
		}

		public override void UnregisterEvents()
		{
		}

		protected virtual void onPointerDown(PointerDownEvent evt)
		{
		}

		protected virtual void onPointerUp(PointerUpEvent evt)
		{
		}

		protected virtual void onPointerEnter(PointerEnterEvent evt)
		{
		}

		protected virtual void onPointerLeave(PointerLeaveEvent evt)
		{
		}

		protected virtual void onFocus(FocusEvent evt)
		{
		}

		protected virtual void onBlur(BlurEvent evt)
		{
		}

		protected virtual void onKeyDown(KeyDownEvent evt)
		{
		}

		protected virtual void onKeyUp(KeyUpEvent evt)
		{
		}

		protected virtual void onChangeBool(ChangeEvent<bool> evt)
		{
		}

		protected virtual void onChangeInt(ChangeEvent<int> evt)
		{
		}

		protected virtual void onChangeFloat(ChangeEvent<float> evt)
		{
		}

		protected virtual void onChangeString(ChangeEvent<string> evt)
		{
		}
	}
}
