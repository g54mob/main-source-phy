using UnityEngine;

namespace Selectors
{
	public class ValueHolderScroll : ValueHolderCentering
	{
		public float step = 0.1f;

		public float[] keptSteps;

		protected bool mouseOver;

		protected override void Update()
		{
			base.Update();
			if (!mouseOver)
			{
				return;
			}
			if (InputManager.ScrollFieldValue() > 0f)
			{
				float num = float.PositiveInfinity;
				for (int i = 0; i < keptSteps.Length; i++)
				{
					if (keptSteps[i] > base.ValueNumber)
					{
						num = keptSteps[i];
						break;
					}
				}
				float valueNumber = base.ValueNumber;
				valueNumber += step;
				if (num < valueNumber)
				{
					valueNumber = num;
				}
				valueNumber = Mathf.Clamp(valueNumber, minValue, maxValue);
				SetValue(valueNumber);
				OnValueChanged();
			}
			else
			{
				if (!(InputManager.ScrollFieldValue() < 0f))
				{
					return;
				}
				float num = 0f;
				for (int num2 = keptSteps.Length - 1; num2 >= 0; num2--)
				{
					if (keptSteps[num2] < base.ValueNumber)
					{
						num = keptSteps[num2];
						break;
					}
				}
				float valueNumber = base.ValueNumber;
				valueNumber -= step;
				if (num > valueNumber)
				{
					valueNumber = num;
				}
				valueNumber = Mathf.Clamp(valueNumber, minValue, maxValue);
				SetValue(valueNumber);
				OnValueChanged();
			}
		}

		protected virtual void OnMouseEnter()
		{
			mouseOver = true;
			StatMaster.DisableCameraZoom(true);
		}

		protected virtual void OnMouseExit()
		{
			mouseOver = false;
			StatMaster.DisableCameraZoom(false);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (mouseOver)
			{
				StatMaster.DisableCameraZoom(false);
			}
		}
	}
}
