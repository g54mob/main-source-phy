using UnityEngine;
using cakeslice;

namespace Selectors
{
	public class LimitsSelector : Selector
	{
		[SerializeField]
		private CircularSlider minSlider;

		[SerializeField]
		private CircularSlider maxSlider;

		[SerializeField]
		private CircularSlider minSliderBackground;

		[SerializeField]
		private CircularSlider maxSliderBackground;

		[SerializeField]
		private SliderHolder minHolder;

		[SerializeField]
		private SliderHolder maxHolder;

		[SerializeField]
		private Transform visHolder;

		[SerializeField]
		private GameObject visual;

		[SerializeField]
		private Material stencilMat;

		private bool updateCallback;

		private bool inConflict;

		public override MapperType MapperType
		{
			get
			{
				return Limits;
			}
			set
			{
				if (updateCallback)
				{
					if (Limits != null)
					{
						Limits.LimitsChanged -= OnLimitsUpdate;
					}
					updateCallback = false;
				}
				Limits = (MLimits)value;
				if (Limits != null)
				{
					Limits.LimitsChanged += OnLimitsUpdate;
					updateCallback = true;
				}
			}
		}

		public MLimits Limits { get; set; }

		private void Awake()
		{
			minSlider.ValueChanged += MinSlider_ValueChanged;
			maxSlider.ValueChanged += MaxSlider_ValueChanged;
			minSlider.DoneEditing += OnEdit;
			maxSlider.DoneEditing += OnEdit;
			minHolder.ValueChanged += MinHolder_ValueChanged;
			maxHolder.ValueChanged += MaxHolder_ValueChanged;
		}

		public override void Init()
		{
			CircularSlider circularSlider = minSlider;
			float num = 0f;
			maxSliderBackground.Min = num;
			num = num;
			minSliderBackground.Min = num;
			num = num;
			maxSlider.Min = num;
			circularSlider.Min = num;
			CircularSlider circularSlider2 = minSlider;
			num = 360f;
			maxSliderBackground.Max = num;
			num = num;
			minSliderBackground.Max = num;
			num = num;
			maxSlider.Max = num;
			circularSlider2.Max = num;
			minSlider.Value = Limits.Min;
			maxSlider.Value = Limits.Max;
			SliderHolder sliderHolder = minHolder;
			num = 0f;
			maxHolder.Min = num;
			sliderHolder.Min = num;
			SliderHolder sliderHolder2 = minHolder;
			num = Limits.MaxValue;
			maxHolder.Max = num;
			sliderHolder2.Max = num;
			CircularSlider circularSlider3 = minSlider;
			num = Limits.MaxValue;
			maxSlider.MaxValue = num;
			circularSlider3.MaxValue = num;
			CircularSlider circularSlider4 = minSliderBackground;
			num = Limits.MaxValue;
			maxSliderBackground.Value = num;
			circularSlider4.Value = num;
			if (visual != null)
			{
				Object.DestroyImmediate(visual);
			}
			visual = Object.Instantiate(Limits.LimitsDisplay.GetLimitsDisplay()).gameObject;
			Object.DestroyImmediate(visual.GetComponent<Outline>());
			visual.SetLayerRecursively(LayerMask.NameToLayer("HUD (Late)"));
			visual.tag = "BlockIcon";
			MeshRenderer component = visual.GetComponent<MeshRenderer>();
			stencilMat.SetTexture("_MainTex", component.material.GetTexture("_MainTex"));
			stencilMat.SetColor("_Color", component.material.GetColor("_Color"));
			stencilMat.SetColor("_RimColor", component.material.GetColor("_RimColor"));
			stencilMat.SetFloat("_RimPower", component.material.GetFloat("_RimPower"));
			component.material = stencilMat;
			visual.transform.parent = visHolder;
			visual.transform.localPosition = Limits.iconInfo.localPosition;
			visual.transform.localRotation = Limits.iconInfo.localRotation;
			visual.transform.localScale = Limits.iconInfo.localScale;
			inConflict = InConflict();
			minHolder.SetConflict(inConflict);
			maxHolder.SetConflict(inConflict);
			base.Init();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Limits != null)
				{
					Limits.LimitsChanged -= OnLimitsUpdate;
				}
				updateCallback = false;
			}
		}

		private void OnLimitsUpdate()
		{
			if (minSlider.Value != Limits.Min)
			{
				minSlider.Value = Limits.Min;
			}
			if (maxSlider.Value != Limits.Max)
			{
				maxSlider.Value = Limits.Max;
			}
			inConflict = InConflict();
			minHolder.SetConflict(inConflict);
			maxHolder.SetConflict(inConflict);
		}

		private void MinHolder_ValueChanged(float newValue)
		{
			if (minSlider.Value != newValue || inConflict)
			{
				minSlider.Value = newValue;
				OnEdit();
			}
		}

		private void MaxHolder_ValueChanged(float newValue)
		{
			if (maxSlider.Value != newValue || inConflict)
			{
				maxSlider.Value = newValue;
				OnEdit();
			}
		}

		private void MinSlider_ValueChanged(float value)
		{
			minHolder.SetText(Mathf.RoundToInt(value));
			if (Limits != null)
			{
				Limits.Min = value;
			}
		}

		private void MaxSlider_ValueChanged(float value)
		{
			maxHolder.SetText(Mathf.RoundToInt(value));
			if (Limits != null)
			{
				Limits.Max = value;
			}
		}
	}
}
