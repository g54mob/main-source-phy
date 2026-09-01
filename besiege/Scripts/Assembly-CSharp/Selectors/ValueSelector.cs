using UnityEngine;

namespace Selectors
{
	public class ValueSelector : Selector
	{
		[SerializeField]
		private DynamicText title;

		[SerializeField]
		private Transform valueBackground;

		[SerializeField]
		private ValueHolder valueHolder;

		[SerializeField]
		private UIButton dragButton;

		[SerializeField]
		private Texture2D cursorTexture;

		private bool updateCallback;

		private float mouseDragStart;

		private float screenWidth;

		private float newValue;

		private bool isDragging;

		private Vector2 hotspot = new Vector2(16f, 16f);

		public override MapperType MapperType
		{
			get
			{
				return mValue;
			}
			set
			{
				if (updateCallback)
				{
					if (mValue != null)
					{
						mValue.ValueChanged -= OnValueChanged;
					}
					updateCallback = false;
				}
				mValue = (MValue)value;
				if (mValue != null)
				{
					mValue.ValueChanged += OnValueChanged;
					updateCallback = true;
				}
			}
		}

		public MValue mValue { get; set; }

		public float Min
		{
			get
			{
				return (mValue == null) ? 0f : mValue.Min;
			}
		}

		public float Max
		{
			get
			{
				return (mValue == null) ? 0f : mValue.Max;
			}
		}

		private void Awake()
		{
			dragButton.Held += OnDragging;
			dragButton.Down += OnDown;
			dragButton.MouseEnter += MouseEnter;
			dragButton.MouseExit += MouseExit;
			dragButton.Released += Released;
			valueHolder.ValueChanged += OnManualInput;
		}

		private void OnDown()
		{
			mouseDragStart = InputManager.CursorPosition().x;
			screenWidth = Screen.currentResolution.width;
		}

		private void OnDragging()
		{
			float num = (InputManager.CursorPosition().x - mouseDragStart) / screenWidth;
			if ((double)num > 0.01 || (double)num < -0.01)
			{
				isDragging = true;
				newValue = ((!((double)num > 0.01)) ? num : Mathf.Pow(num, 1.5f)) * ((!(newValue + mValue.Value > 0.1f) && !(newValue + mValue.Value < -0.1f)) ? 0.1f : Mathf.Abs(mValue.Value));
				UpdateText(newValue + mValue.Value);
			}
		}

		private void Released()
		{
			if (isDragging)
			{
				isDragging = false;
				if (base.gameObject.activeInHierarchy)
				{
					mValue.Value += newValue;
					OnValueChanged(mValue.Value);
					OnEdit();
				}
			}
		}

		private void MouseEnter()
		{
			Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
		}

		private void MouseExit()
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

		private void OnValueChanged(float newValue)
		{
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			UpdateText();
		}

		private void OnManualInput(float value)
		{
			mValue.Value = value;
			OnEdit();
		}

		private void OnDisable()
		{
			if (updateCallback)
			{
				if (mValue != null)
				{
					mValue.ValueChanged -= OnValueChanged;
				}
				updateCallback = false;
			}
		}

		private void UpdateText(float number)
		{
			if (mValue != null && mValue.clampValue)
			{
				valueHolder.SetValue(Mathf.Clamp(number, Min, Max));
				return;
			}
			valueHolder.SetValue(number);
			valueHolder.SetConflict(InConflict());
		}

		private void UpdateText()
		{
			if (mValue != null)
			{
				if (mValue.clampValue)
				{
					valueHolder.SetValue(Mathf.Clamp(mValue.Value, Min, Max));
				}
				else
				{
					valueHolder.SetValue(mValue.Value);
				}
			}
		}

		public override void Init()
		{
			if (mValue == null)
			{
				Debug.LogWarning("mValue has not been assigned to " + base.transform.name);
			}
			else
			{
				title.SetText(mValue.DisplayName.Replace(" ", "\n").ToUpper());
			}
			base.Init();
			UpdateVisual();
		}
	}
}
