using System.Collections;
using UnityEngine;

namespace ModIO.UI
{
	public class EmptyTextToggle : MonoBehaviour
	{
		public enum StatePolarity
		{
			OffIfNullOrEmpty = 0,
			OnIfNullOrEmpty = 1
		}

		public StateToggleDisplay targetToggle;

		public GenericTextComponent textComponent;

		public StatePolarity polarity;

		private void Awake()
		{
		}

		private void OnEnable()
		{
			UpdateToggleState();
		}

		public void UpdateToggleState()
		{
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(StartToggleUpdates());
			}
			else
			{
				UpdateToggleState_Internal();
			}
		}

		private IEnumerator StartToggleUpdates()
		{
			UpdateToggleState_Internal();
			yield return null;
			UpdateToggleState_Internal();
		}

		private void UpdateToggleState_Internal()
		{
			bool flag = string.IsNullOrEmpty(textComponent.text);
			bool isOn = (polarity == StatePolarity.OnIfNullOrEmpty && flag) || (polarity == StatePolarity.OffIfNullOrEmpty && !flag);
			targetToggle.isOn = isOn;
		}
	}
}
